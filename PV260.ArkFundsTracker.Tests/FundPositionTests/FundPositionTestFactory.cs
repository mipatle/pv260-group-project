using System.ComponentModel.DataAnnotations;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Tests.FundPositionTests;

public static class FundPositionTestFactory
{
    public static bool IsValid(FundPosition position)
    {
        var context = new ValidationContext(position);
        var results = new List<ValidationResult>();

        return Validator.TryValidateObject(
            position,
            context,
            results,
            validateAllProperties: true
        );
    }

    public static List<string> GetErrors(FundPosition position)
    {
        var context = new ValidationContext(position);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(
            position,
            context,
            results,
            true
        );

        return results.Select(r => r.ErrorMessage ?? "Unknown error").ToList();
    }

    public static FundPosition CreatePosition(string ticker = "ARKK", decimal shares = 1m, DateOnly? date = null)
        => new()
        {
            Ticker = ticker,
            Shares = shares,
            Date = date ?? new DateOnly(2026, 4, 19),
            Fund = "ARKK",
            Company = "Test company",
            Cusip = "123456789",
            MarketValue = 100m,
            WeightPercentage = 1m
        };
}