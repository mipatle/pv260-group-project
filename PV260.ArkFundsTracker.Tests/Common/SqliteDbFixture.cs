using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;

namespace PV260.ArkFundsTracker.Tests.Common;

public class SqliteDbFixture : IAsyncLifetime
{
    private SqliteConnection Connection { get; set; } = null!;
    public AppDbContext Context { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Connection = new SqliteConnection("Data Source=:memory:");
        await Connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(Connection)
            .Options;

        Context = new AppDbContext(options);

        await Context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
        await Connection.DisposeAsync();
    }

    public async Task ResetAsync()
    {
        await Context.FundPositions
            .IgnoreQueryFilters()
            .ExecuteDeleteAsync();

        await Context.Users
            .IgnoreQueryFilters()
            .ExecuteDeleteAsync();
    }
}