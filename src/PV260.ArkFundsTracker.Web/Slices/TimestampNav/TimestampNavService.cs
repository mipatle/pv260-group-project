using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavService(
    AppDbContext db,
    TimestampCompareService timestampCompareService)
{
    public async Task<TimestampNavViewModel> FillTimestampNavViewModel(DateOnly? firstDate,
        List<DateOnly> dateList,
        CancellationToken ct = default)
    {
        var finalFirstDate = firstDate ?? dateList[1];
        var timestampCompareList = await timestampCompareService.FillComparedPositionsList(finalFirstDate, ct);
        var sortedTimestampCompareList = timestampCompareList
            .OrderBy(x => x.PositionState).ThenByDescending(y => Math.Abs(y.SharesDifferencePercentage)).ToList();

        return new TimestampNavViewModel
        {
            SelectedDate = finalFirstDate,
            DateList = dateList,
            ComparedPositions = sortedTimestampCompareList
        };
    }

    public async Task<List<DateOnly>> GetFirstDateList(CancellationToken ct = default)
    {
        var dateList = await db.FundPositions.Select(x => x.Date)
            .Distinct()
            .OrderByDescending(x => x)
            .ToListAsync(ct);

        return dateList.Count <= 1
            ? throw new DataWithWrongValueException("To compare must be two timestamps minimal.")
            : dateList;
    }
}