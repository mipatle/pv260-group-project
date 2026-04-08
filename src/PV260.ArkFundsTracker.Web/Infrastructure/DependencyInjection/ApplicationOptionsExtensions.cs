using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;

namespace PV260.ArkFundsTracker.Web.Infrastructure.DependencyInjection;

internal static class ApplicationOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<ApplicationOptions>()
            .Bind(configuration.GetSection(ApplicationOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Name),
                $"{ApplicationOptions.SectionName}:{nameof(ApplicationOptions.Name)} must be configured.")
            .ValidateOnStart();

        return services;
    }
}