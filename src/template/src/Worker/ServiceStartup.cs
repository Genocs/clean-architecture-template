using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.ExternalServices;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.Resiliency;
using Genocs.CleanArchitecture.Template.Worker.Configurator;
using Genocs.CleanArchitecture.Template.Worker.ExternalServices;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genocs.CleanArchitecture.Template.Worker;

public static class ServiceStartup
{
    private const string HealthCheckName = "ReadinessLiveness";
    private const string HealthCheckServiceAssembly = "Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckPublisherHostedService";

    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddOptions();

        // Configure AppInsights
        services.AddApplicationInsightsKubernetesEnricher();
        services.AddApplicationInsightsTelemetry(context.Configuration);

        // Setup the Enterprise service bus
#if Rebus
        RebusServiceBusConfigurator.ConfigureServices(context, services);
#elif MassTransit
        MassTransitServiceBusConfigurator.ConfigureServices(context, services);
#elif NServiceBus
        ParticularServiceBusConfigurator.ConfigureServices(context, services);
#elif AzureServiceBus
        AzureServiceBusConfigurator.ConfigureServices(context, services);
#endif

        // Register API client
        services
            .AddHttpClient<IDummyApiClient, DummyApiClient>(c =>
            {
                c.BaseAddress = new Uri(context.Configuration["ExternalWebServices:Order"]);
            })
            .AddResiliencyPolicies(context.Configuration);

        // Register Auth API client
        services
            .AddHttpClient<IAuthApiClient, AuthApiClient>(c =>
            {
                c.BaseAddress = new Uri(context.Configuration["ExternalWebServices:Basket"]);
                c.DefaultRequestHeaders.Add("Authorization", "Bearer your-token");
            })
            .AddResiliencyPolicies(context.Configuration);

        // Add health checks
        services.AddHealthChecks().AddCheck(
                HealthCheckName,
                () => HealthCheckResult.Healthy("OK"));

        if (context.Configuration["HealthCheckInitialDelay"] is var configuredDelay &&
            double.TryParse(configuredDelay, out double delay))
        {
            services.Configure<HealthCheckPublisherOptions>(options =>
                {
                    options.Delay = TimeSpan.FromMilliseconds(delay);
                });
        }

        // workaround .NET Core 2.2: for more info https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/host-and-deploy/health-checks/samples/2.x/HealthChecksSample/LivenessProbeStartup.cs#L51
        services.TryAddEnumerable(
            ServiceDescriptor.Singleton(
                                        typeof(IHostedService),
                                        typeof(HealthCheckPublisherOptions).Assembly
                                            .GetType(HealthCheckServiceAssembly)));

        services.AddSingleton<IHealthCheckPublisher, ReadinessLivenessPublisher>();
    }
}
