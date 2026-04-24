using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavService
{
    private readonly AppDbContext _db;
    private readonly TimestampCompareService _timestampCompareService;

    public TimestampNavService(AppDbContext db, TimestampCompareService timestampCompareService)
    {
        _db = db;
        _timestampCompareService = timestampCompareService;
    }

    public async Task<TimestampNavViewModel> FillTimestampNavViewModel(DateOnly? firstDate)
    {
        var dateList = await _db.FundPositions.Select(x => x.Date)
            .Distinct()
            .OrderByDescending(x => x)
            .ToListAsync();

        if (dateList.Count <= 1)
        {
            throw new DataWithWrongValueException("To compare must be two timestamps minimal.");
        }

        var finalFirstDate = firstDate ?? dateList[1];
        var timestampCompareList = await _timestampCompareService.FillComparedPositionsList(finalFirstDate);
        var sortedTimestampCompareList = timestampCompareList
            .OrderBy(x => x.PositionState).ThenByDescending(y => Math.Abs(y.SharesDifferencePercentage)).ToList();

        return new TimestampNavViewModel
        {
            SelectedDate = finalFirstDate,
            DateList = dateList,
            ComparedPositions = sortedTimestampCompareList
        };
    }
}