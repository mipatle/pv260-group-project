using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Infrastructure.Logging;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class FundPositionsService(AppDbContext db, HttpClient http, ILogger<FundPositionsService> logger)
{
    private const string ArkUrl =
        "https://assets.ark-funds.com/fund-documents/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";

    public async Task<List<FundPosition>> GetHistory(DateOnly date, CancellationToken ct = default)
    {
        return await db.FundPositions.Where(f => f.Date == date).ToListAsync(ct);
    }

    public async Task<List<FundPosition>> FetchAndSaveLatest(int? adminId = null, CancellationToken ct = default)
    {
        var positionsData = await FetchLatestPositions(ct);
        var positions = ParseArkCsv(positionsData);

        if (positions.Count == 0)
        {
            throw new DataInconsistentException("Positions were empty.");
        }

        if (positions.First().Date != DateOnly.FromDateTime(DateTime.Today))
        {
            throw new DataNotLatestException(positions.First().Date, DateOnly.FromDateTime(DateTime.Today));
        }

        var latestPositions = await SetDailyPositions(positions, adminId, ct);
        return latestPositions;
    }

    private async Task<List<FundPosition>> SetDailyPositions(List<FundPosition> positions, int? adminId = null,
        CancellationToken ct = default)
    {
        if (positions.Count == 0)
        {
            return positions;
        }

        ct.ThrowIfCancellationRequested();

        var firstPosition = positions.First();
        if (positions.Any(position => position.Date != firstPosition.Date))
        {
            throw new DataInconsistentException("Positions must all have the same date.");
        }

        await using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);
        try
        {
            var oldPositions = await db.FundPositions
                .Where(position => position.Date == firstPosition.Date)
                .ToListAsync(ct);

            var deletedAt = DateTime.UtcNow;
            foreach (var oldPosition in oldPositions)
            {
                oldPosition.DeletedAt = deletedAt;
            }

            foreach (var position in positions)
            {
                position.Id = Guid.NewGuid();
                position.AdminId = adminId;
            }

            await db.FundPositions.AddRangeAsync(positions, ct);
            await db.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync(ct);
            throw new DataInconsistentException(
                $"Concurrent update detected for date {firstPosition.Date}. {ex.Message}");
        }

        return positions;
    }

    private async Task<string> FetchLatestPositions(CancellationToken ct)
    {
        string csvData;
        if (http.DefaultRequestHeaders.UserAgent.Count == 0)
        {
            http.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        }

        try
        {
            csvData = await http.GetStringAsync(ArkUrl, ct);
        }
        catch (HttpRequestException e)
        {
            Log.HttpFetchingError(logger, e.Message);
            throw new DataUnavailableException(e.Message);
        }

        return csvData;
    }

    private List<FundPosition> ParseArkCsv(string csvContent)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower(CultureInfo.InvariantCulture),
            HasHeaderRecord = true,
            BadDataFound = null,
            ShouldSkipRecord = args =>
            {
                var firstField = args.Row.GetField(0);
                return string.IsNullOrWhiteSpace(firstField) || firstField.Contains("Investors should");
            }
        };

        using var reader = new StringReader(csvContent);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<FundPositionMap>();

        try
        {
            return csv.GetRecords<FundPosition>().ToList();
        }
        catch (CsvHelperException ex)
        {
            var rowNumber = ex.Context?.Parser?.Row;
            var rawRecord = ex.Context?.Parser?.RawRecord;

            if (rowNumber != null && rawRecord != null)
            {
                Log.RowParsingError(logger, (int)rowNumber, rawRecord, ex.Message);
            }
            else
            {
                Log.CsvParsingError(logger, ex.Message);
            }

            throw new DataInconsistentException(ex.Message);
        }
    }
}