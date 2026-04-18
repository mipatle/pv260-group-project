using System.Globalization;
using System.Net.Http.Headers;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Infrastructure.Logging;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class FundPositionsService(AppDbContext db, HttpClient http, ILogger<FundPositionsService> logger)
{
    private readonly HttpClient _http = http;

    private const string ArkUrl =
        "https://assets.ark-funds.com/fund-documents/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv";

    public async Task<List<FundPosition>> GetHistory(DateOnly date)
    {
        return await db.FundPositions.Where(f => f.Date == date).ToListAsync();
    }

    public async Task<List<FundPosition>> FetchAndSaveLatest(int? adminId = null)
    {
        var positionsData = await FetchLatestPositions();
        var positions = ParseArkCsv(positionsData);

        if (positions.Count == 0)
        {
            throw new DataInconsistentException("Positions were empty.");
        }

        if (positions.First().Date != DateOnly.FromDateTime(DateTime.Today))
        {
            throw new DataNotLatestException(positions.First().Date, DateOnly.FromDateTime(DateTime.Today));
        }

        var latestPositions = await SetDailyPositions(positions, adminId);
        return latestPositions;
    }

    public async Task<List<FundPosition>> SetDailyPositions(List<FundPosition> positions, int? adminId = null)
    {
        if (positions.Count == 0)
        {
            return positions;
        }

        var firstPosition = positions.First();
        if (positions.Any(position => position.Date != firstPosition.Date))
        {
            throw new DataInconsistentException("Positions must all have the same date.");
        }

        var oldPositions = await db.FundPositions
            .Where(position => position.Date == firstPosition.Date)
            .ToListAsync();

        foreach (var oldPosition in oldPositions)
        {
            oldPosition.DeletedAt = DateTime.UtcNow;
        }

        foreach (var position in positions)
        {
            position.Id = Guid.NewGuid();
            position.AdminId = adminId;
        }

        await db.FundPositions.AddRangeAsync(positions);

        await db.SaveChangesAsync();

        return positions;
    }

    private async Task<string> FetchLatestPositions()
    {
        string csvData;
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

        try
        {
            csvData = await client.GetStringAsync(ArkUrl);
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