using System.ComponentModel.DataAnnotations;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class FundPosition
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }

    [MaxLength(10)]
    public string Ticker { get; set; } = string.Empty;

    public string Fund { get; set; } = "ARKK";
    public string Company { get; set; } = string.Empty;
    public string Cusip { get; set; } = string.Empty;
    public decimal Shares { get; set; }
    public decimal MarketValue { get; set; }
    public decimal WeightPercentage { get; set; }
    public int? AdminId { get; set; }
    public DateTime? DeletedAt { get; set; }
}