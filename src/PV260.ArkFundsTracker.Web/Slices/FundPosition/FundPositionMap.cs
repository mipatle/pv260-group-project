using CsvHelper.Configuration;
using System.Globalization;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public sealed class FundPositionMap : ClassMap<FundPosition>
{
    public FundPositionMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Date).Name("date");
        Map(m => m.Fund).Name("fund");
        Map(m => m.Company).Name("company");
        Map(m => m.Ticker).Name("ticker");
        Map(m => m.Cusip).Name("cusip");
        Map(m => m.Shares).Name("shares");
        Map(m => m.MarketValue).Name("market value ($)")
            .TypeConverterOption.NumberStyles(NumberStyles.Currency)
            .TypeConverterOption.CultureInfo(CultureInfo.GetCultureInfo("en-US"));
        Map(m => m.WeightPercentage).Name("weight (%)")
            .Convert(args =>
            {
                var rawValue = args.Row.GetField("weight (%)")?.Replace("%", "").Trim();
                var parseSuccesful = decimal.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var result);
                return parseSuccesful ? result : 0m;
            });

        Map(m => m.Id).Ignore();
        Map(m => m.AdminId).Ignore();
        Map(m => m.DeletedAt).Ignore();
    }
}