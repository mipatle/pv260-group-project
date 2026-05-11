namespace PV260.ArkFundsTracker.Web.Infrastructure.Configuration;

public sealed class ApplicationOptions
{
    public const string SectionName = "Application";
   
    public string ArkUrl { get; init; } = string.Empty;
    
    public string CronFetchExpression { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}