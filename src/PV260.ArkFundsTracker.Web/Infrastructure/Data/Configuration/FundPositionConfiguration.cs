using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Web.Infrastructure.Data.Configuration;

public class FundPositionConfiguration : IEntityTypeConfiguration<FundPosition>
{
    public void Configure(EntityTypeBuilder<FundPosition> builder)
    {
        builder.ToTable("fund_positions",
            t => { t.HasCheckConstraint("ck_fund_positions_fund", "\"Fund\" = 'ARKK'"); });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Fund).HasDefaultValue("ARKK");
        builder.Property(x => x.Shares).HasColumnType("numeric");
        builder.Property(x => x.MarketValue).HasColumnType("numeric");
        builder.Property(x => x.WeightPercentage).HasColumnType("numeric");
        builder.HasIndex(x => x.Ticker).HasDatabaseName("idx_fund_positions_ticker");
    }
}