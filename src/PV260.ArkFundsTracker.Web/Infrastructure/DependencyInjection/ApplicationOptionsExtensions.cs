using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;

namespace PV260.ArkFundsTracker.Web.Infrastructure.DependencyInjection;

internal static class ApplicationOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var defaultConnection = configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(defaultConnection))
        {
            throw new InvalidOperationException(
                "ConnectionStrings:Default must be configured. For local host-run, set it in appsettings.Development.json " +
                "or user-secrets. For Docker Compose, ensure the web service sets ConnectionStrings__Default.");
        }

        services.AddOptions<ApplicationOptions>()
            .Bind(configuration.GetSection(ApplicationOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Name),
                $"{ApplicationOptions.SectionName}:{nameof(ApplicationOptions.Name)} must be configured.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.CronFetchExpression),
                $"{ApplicationOptions.SectionName}:{nameof(ApplicationOptions.CronFetchExpression)} must be configured.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.ArkUrl),
                $"{ApplicationOptions.SectionName}:{nameof(ApplicationOptions.ArkUrl)} must be configured.")
            .ValidateOnStart();
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(defaultConnection));

        return services;
    }
}