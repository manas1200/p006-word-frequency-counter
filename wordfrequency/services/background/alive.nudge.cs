using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CLOOPS.microservices.Extensions;
using wordfrequency;

namespace wordfrequency.services.background
{
    /// <summary>
    /// Background service that just logs every 30 seconds to indicate that the service is alive
    /// The cron expression is configurable in the app settings
    public class AliveNudge : BackgroundService
    {
        private readonly ILogger<AliveNudge> _logger;
        private readonly AppSettings _appSettings;

        public AliveNudge(ILogger<AliveNudge> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_appSettings.EnableAliveNudge)
            {
                _logger.LogInformation("[AliveNudge]::service disabled by configuration");
                return;
            }
            _logger.LogInformation("[AliveNudge]::service starting with config: Cron={cron}",
                _appSettings.AliveNudgeCron);

            var cronExpression = Util.GetCronExpression(_appSettings.AliveNudgeCron);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (!await cronExpression.AwaitUntilNextOccurrenceAsync(_logger, _appSettings.AliveNudgeCron, "AliveNudgeService", stoppingToken)) { continue; }

                    if (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("[AliveNudge]::service executing nudge");
                    }
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("[CljpsJobCleanupService]::Service shutting down");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[CljpsJobCleanupService]::Error executing cleanup loop");
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
        }
    }
}
