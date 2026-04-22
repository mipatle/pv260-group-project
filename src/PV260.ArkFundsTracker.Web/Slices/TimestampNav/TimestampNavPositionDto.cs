namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavPositionDto
{
    public DateOnly Date { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public decimal Shares { get; set; }
    public decimal WeightPercentage { get; set; }
}