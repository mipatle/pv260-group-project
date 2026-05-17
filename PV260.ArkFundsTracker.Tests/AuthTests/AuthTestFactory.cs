using Microsoft.AspNetCore.Identity;
using PV260.ArkFundsTracker.Tests.Common;
using PV260.ArkFundsTracker.Web.Slices.Auth.Entities;
using PV260.ArkFundsTracker.Web.Slices.Auth.Services;

namespace PV260.ArkFundsTracker.Tests.AuthTests;

public class AuthTestFactory(SqliteDbFixture db)
{
    public AuthService CreateService()
    {
        return new AuthService(
            db.Context,
            new PasswordHasher<AppUser>());
    }
}