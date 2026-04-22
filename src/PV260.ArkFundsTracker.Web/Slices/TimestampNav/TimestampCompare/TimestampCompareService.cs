using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

public class TimestampCompareService
{
    private readonly AppDbContext _db;

    public TimestampCompareService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TimestampCompareDto>> FillComparedPositionsList(DateOnly firstDate)
    {
        var firstPositionsList = await FetchFundPositionList(firstDate);
        var lastPositionsList = await FetchFundPositionList(await GetLastPositionDate());

        var firstDict = firstPositionsList
            .GroupBy(x => (x.Ticker, x.Company))
            .ToDictionary(g => g.Key, g => g.First());
        var lastDict = lastPositionsList
            .GroupBy(x => (x.Ticker, x.Company))
            .ToDictionary(g => g.Key, g => g.First());

        var allKeys = firstPositionsList
            .Select(x => (x.Ticker, x.Company))
            .Union(lastPositionsList.Select(x => (x.Ticker, x.Company)));

        return
        [
            .. allKeys.Select(key =>
            {
                firstDict.TryGetValue(key, out var first);
                lastDict.TryGetValue(key, out var last);

                return ComparePositions(first, last);
            })
        ];
    }

    private async Task<DateOnly> GetLastPositionDate()
    {
        return await _db.FundPositions.MaxAsync(x => x.Date);
    }

    private static TimestampCompareDto ComparePositions(
        TimestampNavPositionDto? firstPosition,
        TimestampNavPositionDto? lastPosition
    )
    {
        if (firstPosition == null)
        {
            return FetchTimestampCompare(firstPosition, lastPosition, 0, TimestampComparePositionState.New);
        }

        if (lastPosition == null)
        {
            return FetchTimestampCompare(firstPosition, lastPosition, 100, TimestampComparePositionState.Sold);
        }

        if (firstPosition.Shares == lastPosition.Shares)
        {
            return FetchTimestampCompare(firstPosition, lastPosition, 0, TimestampComparePositionState.Same);
        }

        if (firstPosition.Shares == 0)
        {
            throw new DataWithWrongValueException("Shares of specific position can't be zero.");
        }

        var sharesDiff = (lastPosition.Shares - firstPosition.Shares) / firstPosition.Shares * 100;
        var positionState = sharesDiff > 0
            ? TimestampComparePositionState.Increased
            : TimestampComparePositionState.Reduced;

        return FetchTimestampCompare(firstPosition, lastPosition, sharesDiff, positionState);
    }

    private static TimestampCompareDto FetchTimestampCompare(TimestampNavPositionDto? firstPosition,
        TimestampNavPositionDto? lastPosition, decimal sharesDiff, TimestampComparePositionState positionState)
    {
        return new TimestampCompareDto
        {
            FirstPosition = firstPosition,
            LastPosition = lastPosition,
            SharesDifferencePercentage = sharesDiff,
            PositionState = positionState
        };
    }

    private async Task<List<TimestampNavPositionDto>> FetchFundPositionList(DateOnly date)
    {
        return await _db.FundPositions
            .Select(x => new TimestampNavPositionDto
            {
                Date = new DateOnly(x.Date.Year, x.Date.Month, x.Date.Day),
                Ticker = x.Ticker,
                Company = x.Company,
                Shares = x.Shares,
                WeightPercentage = x.WeightPercentage
            })
            .Where(x => x.Date == date)
            .ToListAsync();
    }
}