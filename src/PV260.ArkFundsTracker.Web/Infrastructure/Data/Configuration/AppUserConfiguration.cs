using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PV260.ArkFundsTracker.Web.Slices.Authentication.Entities;

namespace PV260.ArkFundsTracker.Web.Infrastructure.Data.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("app_users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("idx_app_users_email");

        builder.Property(x => x.PasswordHash)
            .IsRequired();
    }
}