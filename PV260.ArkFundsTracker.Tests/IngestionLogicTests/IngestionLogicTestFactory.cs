using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PV260.ArkFundsTracker.Tests.Common;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;
using PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;

namespace PV260.ArkFundsTracker.Tests.IngestionLogicTests;

public class IngestionLogicTestFactory(SqliteDbFixture db, string csv)
{
    public FundPositionsService CreateService()
    {
        var client = new HttpClient(new StubHttpMessageHandler(csv));

        var options = new ApplicationOptions
        {
            Name = "ARK Funds Tracker",
            ArkUrl = "https://assets.ark-funds.com/fund-documents/funds-etf-csv/ARK_INNOVATION_ETF_ARKK_HOLDINGS.csv",
            CronFetchExpression = "32 59 23 * * 7"
        };
        
        return new FundPositionsService(
            db.Context,
            client,
            Options.Create(options),
            NullLogger<FundPositionsService>.Instance,
            new FundPositionValidator());
    }
}