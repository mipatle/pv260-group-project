using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using System.Text.RegularExpressions;

namespace PV260.ArkFundsTracker.Web.Infrastructure.DependencyInjection;

internal static partial class ApplicationOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var defaultConnection = ResolveConnectionString(configuration.GetConnectionString("Default"));
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

    private static string? ResolveConnectionString(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return connectionString;
        }

        var missingVariables = new List<string>();
        var resolved = MyRegex().Replace(connectionString, match =>
        {
            var variableName = match.Groups["name"].Value;
            var value = Environment.GetEnvironmentVariable(variableName);
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            missingVariables.Add(variableName);
            return string.Empty;
        });

        if (missingVariables.Count > 0)
        {
            throw new InvalidOperationException(
                $"Missing environment variables for ConnectionStrings:Default: {string.Join(", ", missingVariables.Distinct())}");
        }

        return resolved;
    }

    [GeneratedRegex(@"\$\{(?<name>[A-Za-z_][A-Za-z0-9_]*)\}")]
    private static partial Regex MyRegex();
}