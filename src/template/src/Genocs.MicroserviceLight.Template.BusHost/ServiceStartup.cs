using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;

namespace Genocs.MicroserviceLight.Template.BusHost
{

    using ExternalServices;
    using BusHost.HostServices;
    using RequestProcessing;

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

            services.Configure<Infrastructure.AzureServiceBus.AzureServiceBusConfiguration>(context.Configuration.GetSection("AzureServiceBusSettings"));
            services.Configure<Infrastructure.RebusServiceBus.RebusBusOptions>(context.Configuration.GetSection("RebusBusSettings"));
            services.Configure<Infrastructure.ParticularServiceBus.ParticularServiceBusOptions>(context.Configuration.GetSection("ParticularServiceBusSettings"));

            // The HostService 
            // This is the Service entry point management
            services.AddHostedService<ParticularService>();

            services.AddTransient<IRequestProcessor, RequestProcessor>();

            // Add health check                                                                                                                                                                                                                     │
            services.AddHealthChecks().AddCheck(
                    HealthCheckName,
                    () => HealthCheckResult.Healthy("OK"));

            if (context.Configuration["HEALTHCHECK_INITIAL_DELAY"] is var configuredDelay &&
                double.TryParse(configuredDelay, out double delay))
            {
                services.Configure<HealthCheckPublisherOptions>(options =>
                    {
                        options.Delay = TimeSpan.FromMilliseconds(delay);
                    });
            }

            // Register simple API client 
            services
                .AddHttpClient<ISimpleServiceCaller, SimpleServiceCaller>(c =>
                {
                    c.BaseAddress = new Uri(context.Configuration["SERVICE_URI_SIMPLE"]);
                })
                .AddResiliencyPolicies(context.Configuration);

            // Register Auth API client
            services
                .AddHttpClient<ISimpleAuthServiceCaller, SimpleAuthServiceCaller>(c =>
                {
                    c.BaseAddress = new Uri(context.Configuration["SERVICE_URI_AUTHORIZED"]);
                    c.DefaultRequestHeaders.Add("Authorization", "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiIsImtpZCI6InN2YiJ9.eyJzdWIiOjEsImlhdCI6MTYwNDY1MzIzNiwiZXhwIjoxNjA0NzM5NjM2LCJlbWFpbCI6ImFkbWluQHV0dS5nbG9iYWwiLCJnaXZlbl9uYW1lIjoiQWRtaW4iLCJmYW1pbHlfbmFtZSI6IkFkbWluIiwicHJlZmVycmVkX3VzZXJuYW1lIjoiYWRtaW4iLCJyb2xlcyI6WyJBZG1pbiIsIk1hbmFnZXIiLCJNZW1iZXIiXX0.WV4wEQxyp7tFTFMO-udiVgMrWLx48bDIQ5eZNR85AcPU57GxszUgSkHlTQqAC4GVtGj53ZAyMKPBZn1qt_WCpYrF9DSX5qRVMgflx7e3ZBtzqrDfbINUZQOF5KnNH5pEKUehXG4kLVLz0q7XhtNIkBchmrOAYXIU-rX9lej4Zbc");
                })
                .AddResiliencyPolicies(context.Configuration);

            // workaround .NET Core 2.2: for more info https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/host-and-deploy/health-checks/samples/2.x/HealthChecksSample/LivenessProbeStartup.cs#L51
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton(typeof(IHostedService),
                    typeof(HealthCheckPublisherOptions).Assembly
                        .GetType(HealthCheckServiceAssembly)));

            services.AddSingleton<IHealthCheckPublisher, ReadinessLivenessPublisher>();
        }
    }
}
