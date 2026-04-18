using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data.Seeding;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Web.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

    public DbSet<FundPosition> FundPositions => Set<FundPosition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        modelBuilder.Entity<FundPosition>().HasData(DataSeeder.GetInitialPositions());

        modelBuilder.Entity<FundPosition>()
            .HasQueryFilter(f => f.DeletedAt == null);
    }
}