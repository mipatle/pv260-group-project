using System.Net;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;
using PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;

namespace PV260.ArkFundsTracker.Tests.FundPositionTests;

public class FundPositionsServiceTests
{
    [Fact]
    public async Task GetHistory_WhenMatchingDateExists_ReturnsPositionsForThatDate()
    {
        var selectedDate = new DateOnly(2026, 4, 20);
        var otherDate = new DateOnly(2026, 4, 19);

        await using var dbContext =
            CreateInMemoryDbContext(nameof(GetHistory_WhenMatchingDateExists_ReturnsPositionsForThatDate));

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(selectedDate, "TSLA"),
            CreateFundPosition(selectedDate, "COIN"),
            CreateFundPosition(otherDate, "ROKU"));

        await dbContext.SaveChangesAsync();

        var service = CreateService(dbContext);

        var result = await service.GetHistory(selectedDate);

        Assert.Equal(2, result.Count);
        Assert.All(result, position => Assert.Equal(selectedDate, position.Date));
        Assert.DoesNotContain(result, position => position.Date == otherDate);
    }

    [Fact]
    public async Task GetHistory_WhenNoPositionsExist_ReturnsEmptyList()
    {
        var selectedDate = new DateOnly(2026, 4, 20);

        await using var dbContext = CreateInMemoryDbContext(nameof(GetHistory_WhenNoPositionsExist_ReturnsEmptyList));

        var service = CreateService(dbContext);

        var result = await service.GetHistory(selectedDate);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetHistory_WhenSoftDeletedPositionsExist_DoesNotReturnThem()
    {
        var selectedDate = new DateOnly(2026, 4, 20);

        await using var dbContext =
            CreateInMemoryDbContext(nameof(GetHistory_WhenSoftDeletedPositionsExist_DoesNotReturnThem));

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(selectedDate, "TSLA"),
            CreateFundPosition(selectedDate, "COIN", DateTime.UtcNow));

        await dbContext.SaveChangesAsync();

        var service = CreateService(dbContext);

        var result = await service.GetHistory(selectedDate);

        Assert.Single(result);
        Assert.Equal("TSLA", result[0].Ticker);
        Assert.Null(result[0].DeletedAt);
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenHttpRequestFails_ThrowsDataUnavailableException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FetchAndSaveLatest_WhenHttpRequestFails_ThrowsDataUnavailableException));

        var service = CreateService(
            dbContext,
            httpHandler: new ThrowingHttpMessageHandler(new HttpRequestException("Network failure.")));

        await Assert.ThrowsAsync<DataUnavailableException>(() => service.FetchAndSaveLatest());
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenResponseContainsNoPositions_ThrowsDataInconsistentException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(
                nameof(FetchAndSaveLatest_WhenResponseContainsNoPositions_ThrowsDataInconsistentException));

        var csv = CreateArkCsv();
        var service = CreateService(dbContext, csv);

        var exception = await Assert.ThrowsAsync<DataInconsistentException>(() => service.FetchAndSaveLatest());

        Assert.Equal("Positions were empty.", exception.Message);
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCsvDateIsNotToday_ThrowsDataNotLatestException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FetchAndSaveLatest_WhenCsvDateIsNotToday_ThrowsDataNotLatestException));

        var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        var csv = CreateArkCsv(
            CreateCsvRow(yesterday, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10));

        var service = CreateService(dbContext, csv);

        await Assert.ThrowsAsync<DataNotLatestException>(() => service.FetchAndSaveLatest());
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCsvContainsMultipleDates_ThrowsDataInconsistentException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(
                nameof(FetchAndSaveLatest_WhenCsvContainsMultipleDates_ThrowsDataInconsistentException));

        var today = DateOnly.FromDateTime(DateTime.Today);
        var yesterday = today.AddDays(-1);

        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10),
            CreateCsvRow(yesterday, "COIN", "Coinbase Global Inc.", "987654321", 50, 500, 5));

        var service = CreateService(dbContext, csv);

        var exception = await Assert.ThrowsAsync<DataInconsistentException>(() => service.FetchAndSaveLatest());

        Assert.Equal("Positions must all have the same date.", exception.Message);
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCsvIsMalformed_ThrowsDataInconsistentException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FetchAndSaveLatest_WhenCsvIsMalformed_ThrowsDataInconsistentException));

        var malformedCsv = """
                           date,fund,ticker,company,cusip,shares,market value ($),weight (%)
                           invalid-date,ARKK,TSLA,Tesla Inc.,123456789,100,1000,10
                           """;

        var service = CreateService(dbContext, malformedCsv);

        await Assert.ThrowsAsync<DataInconsistentException>(() => service.FetchAndSaveLatest());
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCsvIsValid_ValidatesParsedPositions()
    {
        var (dbContext, connection) = await CreateSqliteDbContextAsync();
        await using var _ = dbContext;
        await using var __ = connection;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10),
            CreateCsvRow(today, "COIN", "Coinbase Global Inc.", "987654321", 50, 500, 5));

        var validator = new TrackingFundPositionValidator();
        var service = CreateService(dbContext, csv, validator);

        await service.FetchAndSaveLatest();

        Assert.Equal(1, validator.ValidateAllCallCount);
        Assert.NotNull(validator.LastValidatedPositions);
        Assert.Equal(2, validator.LastValidatedPositions!.Count);
        Assert.All(validator.LastValidatedPositions, position => Assert.Equal(today, position.Date));
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenValidatorFails_ThrowsExceptionAndDoesNotSavePositions()
    {
        var (dbContext, connection) = await CreateSqliteDbContextAsync();
        await using var _ = dbContext;
        await using var __ = connection;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10),
            CreateCsvRow(today, "COIN", "Coinbase Global Inc.", "987654321", 50, 500, 5));

        var service = CreateService(dbContext, csv, new ThrowingFundPositionValidator());

        await Assert.ThrowsAsync<DataInconsistentException>(() => service.FetchAndSaveLatest());

        var savedPositions = await dbContext.FundPositions
            .IgnoreQueryFilters()
            .Where(position => position.Date == today)
            .ToListAsync();

        Assert.Empty(savedPositions);
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCsvIsValid_SavesPositionsAndAssignsIdsAndAdminId()
    {
        var (dbContext, connection) = await CreateSqliteDbContextAsync();
        await using var _ = dbContext;
        await using var __ = connection;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10),
            CreateCsvRow(today, "COIN", "Coinbase Global Inc.", "987654321", 50, 500, 5));

        var service = CreateService(dbContext, csv);

        var result = await service.FetchAndSaveLatest(42);

        Assert.Equal(2, result.Count);
        Assert.All(result, position =>
        {
            Assert.NotEqual(Guid.Empty, position.Id);
            Assert.Equal(42, position.AdminId);
            Assert.Equal(today, position.Date);
            Assert.Null(position.DeletedAt);
        });

        var savedPositions = await dbContext.FundPositions
            .Where(position => position.Date == today)
            .OrderBy(position => position.Ticker)
            .ToListAsync();

        Assert.Equal(2, savedPositions.Count);
        Assert.Equal("COIN", savedPositions[0].Ticker);
        Assert.Equal("TSLA", savedPositions[1].Ticker);
        Assert.All(savedPositions, position => Assert.Equal(42, position.AdminId));
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenPositionsForDateAlreadyExist_SoftDeletesOldOnesAndAddsNewOnes()
    {
        var (dbContext, connection) = await CreateSqliteDbContextAsync();
        await using var _ = dbContext;
        await using var __ = connection;

        var today = DateOnly.FromDateTime(DateTime.Today);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(today, "TSLA"),
            CreateFundPosition(today, "ROKU"));

        await dbContext.SaveChangesAsync();

        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 111, 1111, 11),
            CreateCsvRow(today, "COIN", "Coinbase Global Inc.", "987654321", 222, 2222, 22));

        var service = CreateService(dbContext, csv);

        var result = await service.FetchAndSaveLatest(7);

        Assert.Equal(2, result.Count);
        Assert.All(result, position => Assert.Null(position.DeletedAt));
        Assert.Contains(result, position => position.Ticker == "TSLA");
        Assert.Contains(result, position => position.Ticker == "COIN");

        var allPositionsForDate = await dbContext.FundPositions
            .IgnoreQueryFilters()
            .Where(position => position.Date == today)
            .OrderBy(position => position.Ticker)
            .ThenBy(position => position.DeletedAt)
            .ToListAsync();

        Assert.Equal(4, allPositionsForDate.Count);

        var softDeletedPositions = allPositionsForDate.Where(position => position.DeletedAt is not null).ToList();
        var activePositions = allPositionsForDate.Where(position => position.DeletedAt is null).ToList();

        Assert.Equal(2, softDeletedPositions.Count);
        Assert.Equal(2, activePositions.Count);

        Assert.Contains(softDeletedPositions, position => position.Ticker == "TSLA");
        Assert.Contains(softDeletedPositions, position => position.Ticker == "ROKU");

        Assert.Contains(activePositions, position => position.Ticker == "TSLA");
        Assert.Contains(activePositions, position => position.Ticker == "COIN");
        Assert.All(activePositions, position => Assert.Equal(7, position.AdminId));
    }

    [Fact]
    public async Task FetchAndSaveLatest_WhenCancellationIsRequested_ThrowsOperationCanceledException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(
                nameof(FetchAndSaveLatest_WhenCancellationIsRequested_ThrowsOperationCanceledException));

        var today = DateOnly.FromDateTime(DateTime.Today);
        var csv = CreateArkCsv(
            CreateCsvRow(today, "TSLA", "Tesla Inc.", "123456789", 100, 1000, 10));

        var service = CreateService(dbContext, csv);

        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            service.FetchAndSaveLatest(ct: cancellationTokenSource.Token));
    }

    private static FundPositionsService CreateService(
        AppDbContext dbContext,
        string csvResponse = "",
        IValidator? validator = null,
        HttpMessageHandler? httpHandler = null)
    {
        var httpClient = httpHandler is null
            ? new HttpClient(new StaticCsvHttpMessageHandler(csvResponse))
            : new HttpClient(httpHandler);

        var logger = NullLogger<FundPositionsService>.Instance;
        var fundPositionValidator = validator ?? new NoOpFundPositionValidator();

        var options = new ApplicationOptions
        {
            Name = "ARK Funds Tracker",
            ArkUrl = "https://assets.ark-funds.com/fund-documents/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv",
            CronFetchExpression = "32 59 23 * * 7"
        };

        
        return new FundPositionsService(dbContext, httpClient, Options.Create(options), logger, fundPositionValidator);
    }

    private static AppDbContext CreateInMemoryDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private static async Task<(AppDbContext DbContext, SqliteConnection Connection)> CreateSqliteDbContextAsync()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var dbContext = new AppDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();

        return (dbContext, connection);
    }

    private static FundPosition CreateFundPosition(
        DateOnly date,
        string ticker,
        DateTime? deletedAt = null)
    {
        return new FundPosition
        {
            Id = Guid.NewGuid(),
            Date = date,
            Ticker = ticker,
            Fund = "ARKK",
            Company = $"{ticker} Company",
            Cusip = $"CUSIP-{ticker}",
            Shares = 100,
            MarketValue = 1000,
            WeightPercentage = 10,
            DeletedAt = deletedAt
        };
    }

    private static string CreateArkCsv(params string[] rows)
    {
        const string header = "date,fund,ticker,company,cusip,shares,market value ($),weight (%)";
        return string.Join(Environment.NewLine, new[] { header }.Concat(rows));
    }

    private static string CreateCsvRow(
        DateOnly date,
        string ticker,
        string company,
        string cusip,
        decimal shares,
        decimal marketValue,
        decimal weightPercentage)
    {
        return string.Join(",",
            date.ToString("MM/dd/yyyy"),
            "ARKK",
            ticker,
            company,
            cusip,
            shares,
            marketValue,
            weightPercentage);
    }

    private sealed class NoOpFundPositionValidator : IValidator
    {
        public void Validate(FundPosition position)
        {
        }

        public void ValidateAll(IEnumerable<FundPosition> positions)
        {
        }
    }

    private sealed class TrackingFundPositionValidator : IValidator
    {
        public int ValidateAllCallCount { get; private set; }

        public List<FundPosition>? LastValidatedPositions { get; private set; }

        public void Validate(FundPosition position)
        {
        }

        public void ValidateAll(IEnumerable<FundPosition> positions)
        {
            ValidateAllCallCount++;
            LastValidatedPositions = positions.ToList();
        }
    }

    private sealed class ThrowingFundPositionValidator : IValidator
    {
        public void Validate(FundPosition position)
        {
        }

        public void ValidateAll(IEnumerable<FundPosition> positions)
        {
            throw new DataInconsistentException("Validation failed.");
        }
    }

    private sealed class StaticCsvHttpMessageHandler(string csvResponse) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(csvResponse)
            };

            return Task.FromResult(response);
        }
    }

    private sealed class ThrowingHttpMessageHandler(Exception exceptionToThrow) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw exceptionToThrow;
        }
    }
}