namespace PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;

public interface IValidator
{
    void Validate(FundPosition position);
    void ValidateAll(IEnumerable<FundPosition> positions);
}