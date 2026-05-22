using System.ComponentModel.DataAnnotations;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Tests.FundPositionTests;

public static class FundPositionTestFactory
{
    public static List<ValidationResult> Validate(FundPosition position)
    {
        var context = new ValidationContext(position);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(position, context, results, true);

        return results;
    }

    public static List<ValidationResult> ValidatePosition(string ticker = "ARKK", decimal shares = 1m,
        DateOnly? date = null,
        decimal marketValue = 1,
        decimal weightPercentage = 1)
    {
        var model = new FundPosition
        {
            Ticker = ticker,
            Shares = shares,
            Date = date ?? new DateOnly(2026, 4, 19),
            Fund = "ARKK",
            Company = "Test company",
            Cusip = "123456789",
            MarketValue = marketValue,
            WeightPercentage = weightPercentage
        };

        return Validate(model);
    }
}