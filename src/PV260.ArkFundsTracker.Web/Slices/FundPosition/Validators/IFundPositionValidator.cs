namespace PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;

public interface IFundPositionValidator
{
    void Validate(FundPosition position);
    void ValidateAll(IEnumerable<FundPosition> positions);
}