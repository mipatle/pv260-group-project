using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

namespace PV260.ArkFundsTracker.Tests.TimestampTests;

public class TimestampNavServiceTests
{
    [Fact]
    public async Task FillTimestampNavViewModel_WhenLessThanTwoDatesExist_ThrowsDataWithWrongValueException()
    {
        await using var dbContext =
            CreateInMemoryDbContext(
                nameof(FillTimestampNavViewModel_WhenLessThanTwoDatesExist_ThrowsDataWithWrongValueException));

        var onlyDate = new DateOnly(2026, 4, 21);
        await dbContext.FundPositions.AddAsync(CreateFundPosition(onlyDate, "TSLA", "Tesla", 100));
        await dbContext.SaveChangesAsync();

        var compareService = new TimestampCompareService(dbContext);
        var navService = new TimestampNavService(dbContext, compareService);

        var exception = await Assert.ThrowsAsync<DataWithWrongValueException>(() =>
            navService.FillTimestampNavViewModel(null));

        Assert.Equal("To compare must be two timestamps minimal.", exception.Message);
    }

    [Fact]
    public async Task FillTimestampNavViewModel_WhenDateIsNotProvided_UsesSecondNewestDate()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FillTimestampNavViewModel_WhenDateIsNotProvided_UsesSecondNewestDate));

        var oldest = new DateOnly(2026, 4, 19);
        var middle = new DateOnly(2026, 4, 20);
        var newest = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(oldest, "TSLA", "Tesla", 100),
            CreateFundPosition(middle, "TSLA", "Tesla", 110),
            CreateFundPosition(newest, "TSLA", "Tesla", 120));

        await dbContext.SaveChangesAsync();

        var compareService = new TimestampCompareService(dbContext);
        var navService = new TimestampNavService(dbContext, compareService);

        var result = await navService.FillTimestampNavViewModel(null);

        Assert.Equal(middle, result.SelectedDate);
    }

    [Fact]
    public async Task FillTimestampNavViewModel_WhenDateIsProvided_UsesProvidedDate()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FillTimestampNavViewModel_WhenDateIsProvided_UsesProvidedDate));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(latestDate, "TSLA", "Tesla", 120));

        await dbContext.SaveChangesAsync();

        var compareService = new TimestampCompareService(dbContext);
        var navService = new TimestampNavService(dbContext, compareService);

        var result = await navService.FillTimestampNavViewModel(firstDate);

        Assert.Equal(firstDate, result.SelectedDate);
    }

    [Fact]
    public async Task FillTimestampNavViewModel_WhenDatesExist_ReturnsDateListSortedDescending()
    {
        await using var dbContext =
            CreateInMemoryDbContext(nameof(FillTimestampNavViewModel_WhenDatesExist_ReturnsDateListSortedDescending));

        var d1 = new DateOnly(2026, 4, 19);
        var d2 = new DateOnly(2026, 4, 20);
        var d3 = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(d1, "TSLA", "Tesla", 100),
            CreateFundPosition(d2, "TSLA", "Tesla", 110),
            CreateFundPosition(d3, "TSLA", "Tesla", 120));

        await dbContext.SaveChangesAsync();

        var compareService = new TimestampCompareService(dbContext);
        var navService = new TimestampNavService(dbContext, compareService);

        var result = await navService.FillTimestampNavViewModel(d1);

        Assert.Equal([d3, d2, d1], result.DateList);
    }

    [Fact]
    public async Task
        FillTimestampNavViewModel_WhenComparedPositionsAreReturned_SortsByStateThenByAbsolutePercentageDescending()
    {
        await using var dbContext = CreateInMemoryDbContext(
            nameof(
                FillTimestampNavViewModel_WhenComparedPositionsAreReturned_SortsByStateThenByAbsolutePercentageDescending));

        var firstDate = new DateOnly(2026, 4, 20);
        var latestDate = new DateOnly(2026, 4, 21);

        await dbContext.FundPositions.AddRangeAsync(
            CreateFundPosition(firstDate, "AAPL", "Apple", 100),
            CreateFundPosition(firstDate, "MSFT", "Microsoft", 200),
            CreateFundPosition(firstDate, "TSLA", "Tesla", 100),
            CreateFundPosition(firstDate, "ROKU", "Roku", 100),
            CreateFundPosition(latestDate, "AAPL", "Apple", 150), // Increased +50
            CreateFundPosition(latestDate, "MSFT", "Microsoft", 50), // Reduced -75
            CreateFundPosition(latestDate, "TSLA", "Tesla", 100), // Same 0
            CreateFundPosition(latestDate, "NVDA", "Nvidia", 70) // New 0
        );

        await dbContext.SaveChangesAsync();

        var compareService = new TimestampCompareService(dbContext);
        var navService = new TimestampNavService(dbContext, compareService);

        var result = await navService.FillTimestampNavViewModel(firstDate);

        Assert.Equal(5, result.ComparedPositions.Count);

        var expectedOrder = result.ComparedPositions
            .Select(x => x.FirstPosition?.Ticker ?? x.LastPosition!.Ticker)
            .ToList();

        Assert.Equal(new List<string> { "NVDA", "TSLA", "AAPL", "MSFT", "ROKU" }, expectedOrder);
        Assert.Equal(TimestampComparePositionState.New, result.ComparedPositions[0].PositionState);
        Assert.Equal(TimestampComparePositionState.Same, result.ComparedPositions[1].PositionState);
        Assert.Equal(TimestampComparePositionState.Increased, result.ComparedPositions[2].PositionState);
        Assert.Equal(TimestampComparePositionState.Reduced, result.ComparedPositions[3].PositionState);
        Assert.Equal(TimestampComparePositionState.Sold, result.ComparedPositions[4].PositionState);
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