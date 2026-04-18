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
            throw new InvalidOperationException("ConnectionStrings:Default must be configured.");
        }

        services.AddOptions<ApplicationOptions>()
            .Bind(configuration.GetSection(ApplicationOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Name),
                $"{ApplicationOptions.SectionName}:{nameof(ApplicationOptions.Name)} must be configured.")
            .ValidateOnStart();
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(defaultConnection));

        return services;
    }
}