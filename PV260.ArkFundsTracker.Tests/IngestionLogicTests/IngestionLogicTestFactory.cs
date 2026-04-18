using Microsoft.Extensions.Logging.Abstractions;
using PV260.ArkFundsTracker.Tests.Common;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Tests.IngestionLogicTests;

public class IngestionLogicTestFactory(SqliteDbFixture db, string csv)
{
    public FundPositionsService CreateService()
    {
        var client = new HttpClient(new StubHttpMessageHandler(csv));

        return new FundPositionsService(
            db.Context,
            client,
            NullLogger<FundPositionsService>.Instance);
    }
}