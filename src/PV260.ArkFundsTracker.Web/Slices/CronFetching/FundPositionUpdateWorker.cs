using Cronos;
using Microsoft.Extensions.Options;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Infrastructure.Logging;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Web.Slices.CronFetching;

public class FundPositionUpdateWorker(IServiceProvider services, IOptions<ApplicationOptions> options, ILogger<FundPositionUpdateWorker> logger) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cron = CronExpression.Parse(options.Value.CronFetchExpression, CronFormat.IncludeSeconds);

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
        using var scope = services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<FundPositionsService>();
        var today = DateOnly.FromDateTime(DateTime.Today);

        try
        {
            ct.ThrowIfCancellationRequested();

            Log.CronJobStart(logger, today);
            await service.FetchAndSaveLatest(ct: ct);
            Log.CronJobFinished(logger, today);
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown or cancellation should not be treated as a failed cron run.
        }
        catch (Exception ex)
        {
            Log.CronJobFailed(logger, today, ex.Message);
        }
    }
}