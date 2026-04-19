using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Tests.FundPositionTests;

public class FundPositionValidationTests
{
    [Fact]
    public void Ticker_HasMaxLengthTen()
    {
        var property = typeof(FundPosition).GetProperty(nameof(FundPosition.Ticker));

        var maxLength = property?.GetCustomAttribute<MaxLengthAttribute>();

        Assert.NotNull(maxLength);
        Assert.Equal(10, maxLength.Length);
    }

    [Theory]
    [InlineData("ARKK")]
    [InlineData("A")]
    [InlineData("ABCDEFGHIJ")]
    public void Ticker_WithValidLength_PassesMaxLengthConstraint(string ticker)
    {
        var position = FundPositionTestFactory.CreatePosition(ticker: ticker);

        Assert.True(FundPositionTestFactory.IsValid(position));
    }

    [Fact]
    public void Ticker_WithTooLongValue_FailsMaxLengthConstraint()
    {
        var position = FundPositionTestFactory.CreatePosition(ticker: "ABCDEFGHIJK");

        Assert.False(FundPositionTestFactory.IsValid(position));

        var errors = FundPositionTestFactory.GetErrors(position);
        Assert.NotEmpty(errors);
        Assert.Contains(errors, e => e.Contains("Ticker"));
    }
}
