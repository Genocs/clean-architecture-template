using Genocs.CleanArchitecture.Template.Worker.ExternalServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genocs.CleanArchitecture.Template.Worker.HealthCheck;

public static class HealthCheckServicesExtensions
{
    private const string HealthCheckName = "ReadinessLiveness";
    private const string HealthCheckServiceAssembly = "Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckPublisherHostedService";

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        // Add health checks
        services.AddHealthChecks().AddCheck(
                HealthCheckName,
                () => HealthCheckResult.Healthy("OK"));

        // services.AddHealthChecks()
        //    .AddCheck<ExternalWebServiceHealthCheck>("External Web Service", tags: new[] { "external" })
        //    .AddCheck<DatabaseHealthCheck>("Database", tags: new[] { "database" })
        //    .AddCheck<MessageQueueHealthCheck>("Message Queue", tags: new[] { "messagequeue" });

        // workaround .NET Core 2.2: for more info https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/host-and-deploy/health-checks/samples/2.x/HealthChecksSample/LivenessProbeStartup.cs#L51
        services.TryAddEnumerable(
            ServiceDescriptor.Singleton(
                                        typeof(IHostedService),
                                        typeof(HealthCheckPublisherOptions).Assembly
                                            .GetType(HealthCheckServiceAssembly)));

        services.AddSingleton<IHealthCheckPublisher, ReadinessLivenessPublisher>();

        return services;
    }
}
