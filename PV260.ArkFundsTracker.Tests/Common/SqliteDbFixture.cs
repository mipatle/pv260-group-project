using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;

namespace PV260.ArkFundsTracker.Tests.Common;

public class SqliteDbFixture : IAsyncLifetime
{
    private SqliteConnection? Connection { get; set; }
    public AppDbContext? Context { get; private set; }
    
    
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
        if (Context == null || Connection == null)
        {
            return;
        }
        
        await Context!.DisposeAsync();
        await Connection!.DisposeAsync();
    }
    
    public async Task ResetAsync()
    {
        if (Context == null)
        {
            return;
        }
        
        Context.FundPositions.RemoveRange(Context.FundPositions);
        await Context.SaveChangesAsync();
    }
}