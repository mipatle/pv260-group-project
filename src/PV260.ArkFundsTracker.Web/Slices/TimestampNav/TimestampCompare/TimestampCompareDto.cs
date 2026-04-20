namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

public class TimestampCompareDto
{
    public TimestampNavPositionDto? FirstPosition { get; set; }
    public TimestampNavPositionDto? LastPosition { get; set; }
    public decimal SharesDifferancePercentage { get; set; }
    public TimestampComparePositionState PositionState { get; set; }
}