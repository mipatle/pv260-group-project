using System.ComponentModel.DataAnnotations;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;

public class FundPositionValidator : IFundPositionValidator
{
    public void Validate(FundPosition position)
    {
        var context = new ValidationContext(position);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(position, context, results, true))
        {
            throw new DataInconsistentException(
                $"Invalid FundPosition (Ticker: {position.Ticker}, Date: {position.Date}): {string.Join(", ", results.Select(r => r.ErrorMessage))}");
        }
    }

    public void ValidateAll(IEnumerable<FundPosition> positions)
    {
        foreach (var position in positions)
        {
            Validate(position);
        }
    }
}