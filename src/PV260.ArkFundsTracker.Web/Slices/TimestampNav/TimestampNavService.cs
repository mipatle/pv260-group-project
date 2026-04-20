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

        if (dateList.Count != 0)
        {
            dateList.RemoveAt(0);
        }

        var finalFirstDate = firstDate ?? dateList[0];
        var timestampCompareList = await _timestampCompareService.FillComparedPositionsList(finalFirstDate);

        return new TimestampNavViewModel
        {
            DateList = dateList,
            ComperedPositions = timestampCompareList
        };
    }
}