using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavViewModel
{
    public List<TimestampCompareDto> ComparedPositions { get; set; } = [];
    public List<DateOnly> DateList { get; set; } = [];
}