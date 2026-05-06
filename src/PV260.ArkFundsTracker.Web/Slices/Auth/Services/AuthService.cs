using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Slices.Auth;
using PV260.ArkFundsTracker.Web.Slices.Auth.Entities;
using PV260.ArkFundsTracker.Web.Slices.Auth.ViewModels;

namespace PV260.ArkFundsTracker.Web.Slices.Auth.Services;

public class AuthService(
    AppDbContext db,
    PasswordHasher<AppUser> passwordHasher)
{
    public async Task<AppUser?> RegisterAsync(RegisterViewModel model, CancellationToken ct = default)
    {
        var normalizedEmail = model.Email.Trim().ToLowerInvariant();

        var userExists = await db.Users
            .AnyAsync(u => u.Email == normalizedEmail, ct);

        if (userExists)
        {
            return null;
        }

        var user = new AppUser
        {
            Email = normalizedEmail,
            PasswordHash = string.Empty,
            Role = AuthenticationConstants.RoleUser
        };

        user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

        db.Users.Add(user);

        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            return null;
        }

        return user;
    }

    public async Task<AppUser?> LoginAsync(LoginViewModel model, CancellationToken ct = default)
    {
        var normalizedEmail = model.Email.Trim().ToLowerInvariant();

        var user = await db.Users
            .FirstOrDefaultAsync(u => u.Email == normalizedEmail, ct);

        if (user is null)
        {
            return null;
        }

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            model.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException is PostgresException postgresException
               && postgresException.SqlState == PostgresErrorCodes.UniqueViolation;
    }
}