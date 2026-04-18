using Cronos;
using PV260.ArkFundsTracker.Web.Infrastructure.Logging;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Web.Slices.CronFetching;

public class CronJob : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<CronJob> _logger;
    private const string Expression = "0 59 23 * * 7";
    private const int AdminId = 42;  // Temporary Id until the auth gets implemented.

    public CronJob(IServiceProvider services, ILogger<CronJob> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cron = CronExpression.Parse(Expression, CronFormat.IncludeSeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            var next = cron.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);

            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                await DoWork(stoppingToken);
            }
            
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task DoWork(CancellationToken ct)
    {
        using var scope = _services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<FundPositionsService>();
        var today = DateOnly.FromDateTime(DateTime.Today);
        
        try 
        {
            ct.ThrowIfCancellationRequested();
            
            Log.CronJobStart(_logger, today);
            await service.FetchAndSaveLatest(AdminId); 
            Log.CronJobFinished(_logger, today);
        }
        catch (Exception ex)
        {
            Log.CronJobFailed(_logger, today, ex.Message);
        }
    }
}