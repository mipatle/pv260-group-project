using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

namespace PV260.ArkFundsTracker.Tests.TimestampNavTests;

public class TimestampCompareServiceTests
{
    [Fact]
    public async Task FillComparedPositionsList_WhenTickerExistsOnlyInLatestSnapshot_ReturnsNewState()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenTickerExistsOnlyInLatestSnapshot_ReturnsNewState));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "NVDA", "Nvidia", 50));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var nvda = Assert.Single(result, x => x.LastPosition?.Ticker == "NVDA");
        Assert.Equal(TimestampComparePositionState.New, nvda.PositionState);
        Assert.Equal(0, nvda.SharesDifferencePercentage);
        Assert.Null(nvda.FirstPosition);
        Assert.NotNull(nvda.LastPosition);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenTickerExistsOnlyInFirstSnapshot_ReturnsSoldState()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenTickerExistsOnlyInFirstSnapshot_ReturnsSoldState));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "ROKU", "Roku", 75),
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 100));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var roku = Assert.Single(result, x => x.FirstPosition?.Ticker == "ROKU");
        Assert.Equal(TimestampComparePositionState.Sold, roku.PositionState);
        Assert.Equal(-100, roku.SharesDifferencePercentage);
        Assert.NotNull(roku.FirstPosition);
        Assert.Null(roku.LastPosition);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenSharesAreEqual_ReturnsSameState()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenSharesAreEqual_ReturnsSameState));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 100));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var tsla = Assert.Single(result);
        Assert.Equal(TimestampComparePositionState.Same, tsla.PositionState);
        Assert.Equal(0, tsla.SharesDifferencePercentage);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenSharesIncrease_ReturnsIncreasedStateAndCorrectPercentage()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenSharesIncrease_ReturnsIncreasedStateAndCorrectPercentage));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 125));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var tsla = Assert.Single(result);
        Assert.Equal(TimestampComparePositionState.Increased, tsla.PositionState);
        Assert.Equal(25, tsla.SharesDifferencePercentage);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenSharesDecrease_ReturnsReducedStateAndCorrectPercentage()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenSharesDecrease_ReturnsReducedStateAndCorrectPercentage));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 200),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 50));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var tsla = Assert.Single(result);
        Assert.Equal(TimestampComparePositionState.Reduced, tsla.PositionState);
        Assert.Equal(-75, tsla.SharesDifferencePercentage);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenFirstSharesAreZero_ThrowsDataWithWrongValueException()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenFirstSharesAreZero_ThrowsDataWithWrongValueException));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 0),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 100));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var exception = await Assert.ThrowsAsync<DataWithWrongValueException>(() =>
            service.FillComparedPositionsList(firstDate));

        Assert.Equal("Shares of specific position can't be zero.", exception.Message);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenMultipleSnapshotsExist_AlwaysComparesAgainstLatestDate()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenMultipleSnapshotsExist_AlwaysComparesAgainstLatestDate));

        var firstDate = new DateOnly(2026, 4, 19);
        var middleDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(middleDate, "TSLA", "Tesla", 110),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 150));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var tsla = Assert.Single(result);
        Assert.Equal(50, tsla.SharesDifferencePercentage);
        Assert.Equal(TimestampComparePositionState.Increased, tsla.PositionState);
        Assert.Equal(firstDate, tsla.FirstPosition!.Date);
        Assert.Equal(latestDate, tsla.LastPosition!.Date);
    }

    [Fact]
    public async Task FillComparedPositionsList_WhenDuplicateTickerAndCompanyExist_UsesFirstRecordFromEachSnapshot()
    {
        await using var dbContext = CreateInMemoryDbContext(nameof(FillComparedPositionsList_WhenDuplicateTickerAndCompanyExist_UsesFirstRecordFromEachSnapshot));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(firstDate, "TSLA", "Tesla", 999),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 120),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 888));

        await dbContext.SaveChangesAsync();

        var service = new TimestampCompareService(dbContext);

        var result = await service.FillComparedPositionsList(firstDate);

        var tsla = Assert.Single(result);
        Assert.Equal(20, tsla.SharesDifferencePercentage);
        Assert.Equal(TimestampComparePositionState.Increased, tsla.PositionState);
        Assert.Equal(100, tsla.FirstPosition!.Shares);
        Assert.Equal(120, tsla.LastPosition!.Shares);
    }

    private static AppDbContext CreateInMemoryDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;

        return new AppDbContext(options);
    }

    private static FundPosition CreateFundPosition(
        DateOnly date,
        string ticker,
        string company,
        decimal shares)
    {
        return new FundPosition
        {
            Id = Guid.NewGuid(),
            Date = date,
            Ticker = ticker,
            Company = company,
            Fund = "ARKK",
            Cusip = $"CUSIP-{ticker}",
            Shares = shares,
            MarketValue = 1000,
            WeightPercentage = 10
        };
    }
}