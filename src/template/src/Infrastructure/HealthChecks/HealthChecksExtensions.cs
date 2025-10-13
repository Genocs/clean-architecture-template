using Genocs.CleanArchitecture.Template.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genocs.CleanArchitecture.Template.Infrastructure.HealthChecks;

public static class HealthChecksExtensions
{
    /// <summary>
    /// Extension method to add custom health checks to the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration manager.</param>
    /// <returns>The Genocs builder to be used for chain.</returns>
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure health check settings
        services.Configure<HealthCheckSettings>(configuration.GetSection(HealthCheckSettings.Position));

        // Configure comprehensive health checks
        services.AddHealthChecks()
            .AddCheck<ConfigurationHealthCheck>("configuration", tags: new[] { "readiness", "startup" })
            .AddCheck<StartupHealthCheck>("startup", tags: new[] { "readiness", "startup" })
            .AddCheck<MemoryHealthCheck>("memory", tags: new[] { "liveness", "system" })
            //.AddMongoDb(
            //    dbFactory: serviceProvider =>
            //    {
            //        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            //        var mongoDbSettings = new MongoDbOptions();
            //        configuration.GetSection(MongoDbOptions.Position).Bind(mongoDbSettings);
            //        return new MongoDB.Driver.MongoClient(mongoDbSettings.ConnectionString).GetDatabase(mongoDbSettings.Database);
            //    },
            //    name: "mongo",
            //    failureStatus: HealthStatus.Unhealthy,
            //    tags: new[] { "readiness", "database" },
            //    timeout: TimeSpan.FromSeconds(5))
            .AddUrlGroup(
                new Uri("https://httpbin.org/status/200"),
                name: "external-api",
                tags: new[] { "readiness", "external" },
                timeout: TimeSpan.FromSeconds(5));

        // Configure health check publisher options
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            var healthCheckSettings = new HealthCheckSettings();
            configuration.GetSection(HealthCheckSettings.Position).Bind(healthCheckSettings);

            options.Delay = TimeSpan.FromSeconds(2);
            options.Period = TimeSpan.FromSeconds(healthCheckSettings.EvaluationTimeInSeconds);
            options.Timeout = TimeSpan.FromSeconds(10);
            options.Predicate = check => check.Tags.Contains("readiness");
        });

        return services;
    }
}

