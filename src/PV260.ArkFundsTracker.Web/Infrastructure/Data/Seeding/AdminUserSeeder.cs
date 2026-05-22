using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.Auth;
using PV260.ArkFundsTracker.Web.Slices.Auth.Entities;

namespace PV260.ArkFundsTracker.Web.Infrastructure.Data.Seeding;

public sealed partial class AdminUserSeeder(
    AppDbContext db,
    PasswordHasher<AppUser> passwordHasher,
    IOptions<AdminUserOptions> options,
    ILogger<AdminUserSeeder> logger)
{
    public async Task SeedAsync(CancellationToken ct = default)
    {
        var adminOptions = options.Value;

        if (string.IsNullOrWhiteSpace(adminOptions.Email) ||
            string.IsNullOrWhiteSpace(adminOptions.Password))
        {
            AdminSeedingSkipped(logger);
            return;
        }

        var normalizedEmail = adminOptions.Email.Trim().ToLowerInvariant();

        var exists = await db.Users
            .AnyAsync(u => u.Email == normalizedEmail, ct);

        if (exists)
        {
            AdminAlreadyExists(logger);
            return;
        }

        var admin = new AppUser
        {
            Email = normalizedEmail,
            PasswordHash = string.Empty,
            Role = AuthenticationConstants.RoleAdmin
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, adminOptions.Password);

        db.Users.Add(admin);
        await db.SaveChangesAsync(ct);

        AdminSeeded(logger);
    }

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Warning,
        Message = "Admin user seeding skipped because AdminUser configuration is missing.")]
    private static partial void AdminSeedingSkipped(ILogger logger);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Information,
        Message = "Admin user already exists, skipping seeding.")]
    private static partial void AdminAlreadyExists(ILogger logger);

    [LoggerMessage(
        EventId = 2003,
        Level = LogLevel.Information,
        Message = "Admin user seeded successfully.")]
    private static partial void AdminSeeded(ILogger logger);
}