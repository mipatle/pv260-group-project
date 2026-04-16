namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class FundHoldingsViewModel
{
    public List<FundPosition> Positions { get; set; } = new();
    public DateOnly SelectedDate { get; set; }
    public bool IsFromDatabase { get; set; }
}