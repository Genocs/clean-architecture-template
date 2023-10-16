using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genocs.CleanArchitecture.Template.Worker.ExternalServices;

public class ReadinessLivenessPublisher : IHealthCheckPublisher
{
    public const string FilePath = "healthz";

    private readonly ILogger _logger;

    public ReadinessLivenessPublisher(ILogger<ReadinessLivenessPublisher> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task PublishAsync(HealthReport report,
            CancellationToken cancellationToken)
    {
        switch (report.Status)
        {
            case HealthStatus.Healthy:
                {
                    _logger.LogInformation(
                            "{Timestamp} Readiness/Liveness Probe Status: {Result}",
                            DateTime.UtcNow,
                            report.Status);

                    CreateOrUpdateHealthz();

                    break;
                }

            case HealthStatus.Degraded:
                {
                    _logger.LogWarning(
                            "{Timestamp} Readiness/Liveness Probe Status: {Result}",
                            DateTime.UtcNow,
                            report.Status);

                    break;
                }

            case HealthStatus.Unhealthy:
                {
                    _logger.LogError(
                            "{Timestamp} Readiness Probe/Liveness Status: {Result}",
                            DateTime.UtcNow,
                            report.Status);

                    break;
                }
        }

        cancellationToken.ThrowIfCancellationRequested();

        return Task.CompletedTask;
    }

    private static void CreateOrUpdateHealthz()
    {
        if (File.Exists(FilePath))
        {
            File.SetLastWriteTimeUtc(FilePath, DateTime.UtcNow);
        }
        else
        {
            File.AppendText(FilePath).Close();
        }
    }
}
