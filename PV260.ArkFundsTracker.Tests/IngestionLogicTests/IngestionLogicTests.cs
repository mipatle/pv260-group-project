using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Tests.Common;

namespace PV260.ArkFundsTracker.Tests.IngestionLogicTests;

public class IngestionLogicTests(SqliteDbFixture db) : IClassFixture<SqliteDbFixture>
{
    private const string CsvName = "valid_today.csv";

    [Fact]
    public async Task Cron_Save_SetsAdminId_ToNull()
    {
        await db.ResetAsync();

        var factory = new IngestionLogicTestFactory(db, TestDataLoader.LoadCsvWithToday(CsvName));
        var service = factory.CreateService();

        var saved = await service.FetchAndSaveLatest();
        Assert.NotEmpty(saved);
        Assert.All(saved, x => Assert.Null(x.AdminId));

        var fromDb = await db.Context.FundPositions.ToListAsync();
        Assert.NotEmpty(fromDb);
        Assert.All(fromDb, x => Assert.Null(x.AdminId));
    }

    [Fact]
    public async Task Admin_Save_StoresAdminId()
    {
        await db.ResetAsync();

        const int adminId = 42;

        var factory = new IngestionLogicTestFactory(db, TestDataLoader.LoadCsvWithToday(CsvName));
        var service = factory.CreateService();

        var saved = await service.FetchAndSaveLatest(adminId);
        Assert.NotEmpty(saved);
        Assert.All(saved, x => Assert.Equal(adminId, x.AdminId));

        var fromDb = await db.Context.FundPositions.ToListAsync();
        Assert.NotEmpty(fromDb);
        Assert.All(fromDb, x => Assert.Equal(adminId, x.AdminId));
    }
}