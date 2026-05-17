using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Tests.Common;
using PV260.ArkFundsTracker.Web.Slices.Auth;
using PV260.ArkFundsTracker.Web.Slices.Auth.Entities;
using PV260.ArkFundsTracker.Web.Slices.Auth.ViewModels;

namespace PV260.ArkFundsTracker.Tests.AuthTests;

public class AuthServiceTests(SqliteDbFixture db)
    : IClassFixture<SqliteDbFixture>
{
    [Fact]
    public async Task RegisterAsync_Creates_New_User()
    {
        await db.ResetAsync();

        var factory = new AuthTestFactory(db);
        var service = factory.CreateService();
        
        var email = $"{Guid.NewGuid()}@example.com";

        var model = new RegisterViewModel
        {
            Email = email,
            Password = "Password123!"
        };

        var result = await service.RegisterAsync(model);

        Assert.NotNull(result);

        var user = await db.Context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        Assert.NotNull(user);
        Assert.NotEmpty(user.PasswordHash);
        Assert.Equal(AuthenticationConstants.RoleUser, user.Role);
    }

    [Fact]
    public async Task RegisterAsync_Returns_Null_When_Email_Already_Exists()
    {
        var user = await CreateAppUser(AuthenticationConstants.RoleUser, "Password123!");

        db.Context.Users.Add(user);
        await db.Context.SaveChangesAsync();

        var factory = new AuthTestFactory(db);
        var service = factory.CreateService();

        var model = new RegisterViewModel
        {
            Email = user.Email,
            Password = "Password123!"
        };

        var result = await service.RegisterAsync(model);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_Returns_User_When_Credentials_Are_Valid()
    {
        var user = await CreateAppUser(AuthenticationConstants.RoleUser, "Password123!");

        db.Context.Users.Add(user);

        await db.Context.SaveChangesAsync();

        var factory = new AuthTestFactory(db);
        var service = factory.CreateService();

        var model = new LoginViewModel
        {
            Email = user.Email,
            Password = "Password123!"
        };

        var result = await service.LoginAsync(model);

        Assert.NotNull(result);
        Assert.Equal(user.Email, result!.Email);
    }

    [Fact]
    public async Task LoginAsync_Returns_Null_When_Password_Is_Invalid()
    {
        var user = await CreateAppUser(AuthenticationConstants.RoleUser, "CorrectPassword");

        db.Context.Users.Add(user);

        await db.Context.SaveChangesAsync();

        var factory = new AuthTestFactory(db);
        var service = factory.CreateService();

        var model = new LoginViewModel
        {
            Email = user.Email,
            Password = "WrongPassword"
        };

        var result = await service.LoginAsync(model);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_Returns_Null_When_User_Does_Not_Exist()
    {
        await db.ResetAsync();

        var factory = new AuthTestFactory(db);
        var service = factory.CreateService();
        
        var email = $"{Guid.NewGuid()}@example.com";

        var model = new LoginViewModel
        {
            Email = email,
            Password = "Password123!"
        };

        var result = await service.LoginAsync(model);

        Assert.Null(result);
    }

    private async Task<AppUser> CreateAppUser(string role, string password)
    {
        await db.ResetAsync();
        
        var email = $"{Guid.NewGuid()}@example.com";
        
        var passwordHasher = new PasswordHasher<AppUser>();

        var user = new AppUser
        {
            Email = email,
            Role = role,
            PasswordHash = string.Empty
        };

        user.PasswordHash =
            passwordHasher.HashPassword(user, password);
        
        return user;
    }
}