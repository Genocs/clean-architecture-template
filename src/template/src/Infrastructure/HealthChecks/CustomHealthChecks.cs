using Genocs.CleanArchitecture.Template.Infrastructure.Options;
// using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Infrastructure.HealthChecks;

/// <summary>
/// Custom health check for application configuration validation.
/// </summary>
public class ConfigurationHealthCheck(IConfiguration configuration) : IHealthCheck
{
    private readonly IConfiguration _configuration = configuration;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if essential configuration sections exist

            /* Example: Check for RabbitMQ settings
             * You can add checks for other essential configurations as needed
            */

            /*
            var rabbitMQSettings = _configuration.GetSection(MassTransitSetting.Position);
            if (!rabbitMQSettings.Exists())
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("RabbitMQ settings are not configured"));
            }
            */

            /*
             * Add here other configuration sections to validate as needed
            var mongoDbSettings = _configuration.GetSection(Persistence.MongoDb.Configurations.MongoDbOptions.Position);

            if (!mongoDbSettings.Exists())
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("MongoDb settings are not configured"));
            }
            */

            return Task.FromResult(HealthCheckResult.Healthy("Configuration is valid"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Configuration validation failed", ex));
        }
    }
}

/// <summary>
/// Custom health check for application startup readiness.
/// </summary>
public class StartupHealthCheck : IHealthCheck
{
    private static bool _isStartupComplete = false;

    public static void MarkStartupComplete()
    {
        _isStartupComplete = true;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_isStartupComplete)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Application startup is complete"));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("Application startup is not complete"));
    }
}

/// <summary>
/// Custom health check for memory usage monitoring.
/// </summary>
public class MemoryHealthCheck(IOptions<HealthCheckSettings> settings) : IHealthCheck
{
    private readonly HealthCheckSettings _settings = settings.Value;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var memoryInfo = GC.GetGCMemoryInfo();
        long totalMemory = GC.GetTotalMemory(false);
        long totalMemoryMB = totalMemory / 1024 / 1024;

        long warningThreshold = _settings.MemoryThresholdMB * 1024L * 1024L;
        long criticalThreshold = _settings.MemoryCriticalThresholdMB * 1024L * 1024L;

        var data = new Dictionary<string, object>
        {
            { "TotalAllocatedBytes", totalMemory },
            { "TotalAllocatedMB", totalMemoryMB },
            { "WarningThresholdMB", _settings.MemoryThresholdMB },
            { "CriticalThresholdMB", _settings.MemoryCriticalThresholdMB },
            { "Gen0Collections", GC.CollectionCount(0) },
            { "Gen1Collections", GC.CollectionCount(1) },
            { "Gen2Collections", GC.CollectionCount(2) }
        };

        if (totalMemory > criticalThreshold)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Memory usage is critical ({totalMemoryMB}MB)", data: data));
        }

        if (totalMemory > warningThreshold)
        {
            return Task.FromResult(HealthCheckResult.Degraded($"Memory usage is high ({totalMemoryMB}MB)", data: data));
        }

        return Task.FromResult(HealthCheckResult.Healthy($"Memory usage is normal ({totalMemoryMB}MB)", data: data));
    }
}

