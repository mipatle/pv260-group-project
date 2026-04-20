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

    public async Task<List<TimestampCompareDto>> FillComparedPositionsList(DateOnly firstLast)
    {
        var firstPositionsList = await FetchFundPositionList(firstLast);
        var lastPositionsList = await FetchFundPositionList(await GetLastPositionDate());

        var allKeys = firstPositionsList
            .Select(x => (x.Ticker, x.Company))
            .Union(lastPositionsList.Select(x => (x.Ticker, x.Company)));

        var firstDict = firstPositionsList.ToDictionary(x => (x.Ticker, x.Company));
        var lastDict = lastPositionsList.ToDictionary(x => (x.Ticker, x.Company));

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
        decimal sharesDiff = 0;
        var positionState = TimestampComparePositionState.New;
        if (lastPosition == null)
        {
            positionState = TimestampComparePositionState.Reduced;
        }
        else if (firstPosition == null)
        {
            sharesDiff = lastPosition.Shares;
        }
        else
        {
            if (firstPosition.Shares != lastPosition.Shares)
            {
                sharesDiff = (lastPosition.Shares - firstPosition.Shares) / firstPosition.Shares * 100;
                positionState = sharesDiff > 0
                    ? TimestampComparePositionState.Increased
                    : TimestampComparePositionState.Reduced;
            }
            else
            {
                positionState = TimestampComparePositionState.Same;
            }
        }

        return new TimestampCompareDto
        {
            FirstPosition = firstPosition,
            LastPosition = lastPosition,
            SharesDifferancePercentage = sharesDiff,
            PositionState = positionState
        };
    }

    public async Task<List<TimestampNavPositionDto>> FetchFundPositionList(DateOnly date)
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