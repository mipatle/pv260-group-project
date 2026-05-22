using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Tests.FundPositionTests;

public class FundPositionValidationTests
{
    [Theory]
    [InlineData("TOO_LONG_TICKER")]
    [InlineData("12345678901")]
    public void Ticker_LongerThan10Chars_ShouldFail(string ticker)
    {
        var errors = FundPositionTestFactory.ValidatePosition(ticker);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(FundPosition.Ticker)));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1234567890")]
    [InlineData("")]
    public void Ticker_Max10Chars_ShouldPass(string ticker)
    {
        var errors = FundPositionTestFactory.ValidatePosition(ticker);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(-100)]
    public void Shares_NegativeValues_ShouldFail(decimal shares)
    {
        var errors = FundPositionTestFactory.ValidatePosition(shares: shares);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(FundPosition.Shares)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(4242)]
    public void Shares_NonNegativeValues_ShouldPass(decimal shares)
    {
        var errors = FundPositionTestFactory.ValidatePosition(shares: shares);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(-11)]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void MarketValue_NegativeValues_ShouldFail(decimal value)
    {
        var errors = FundPositionTestFactory.ValidatePosition(marketValue: value);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(FundPosition.MarketValue)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(42)]
    public void MarketValue_NonNegativeValues_ShouldPass(decimal value)
    {
        var errors = FundPositionTestFactory.ValidatePosition(marketValue: value);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void WeightPercentage_OutOfRange_ShouldFail(decimal value)
    {
        var errors = FundPositionTestFactory.ValidatePosition(weightPercentage: value);

        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(FundPosition.WeightPercentage)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(100)]
    public void WeightPercentage_Between0And100_ShouldPass(decimal value)
    {
        var errors = FundPositionTestFactory.ValidatePosition(weightPercentage: value);

        Assert.Empty(errors);
    }
}