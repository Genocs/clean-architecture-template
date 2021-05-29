namespace Genocs.MicroserviceLight.Template.BusWorker
{
    using Application.Services;
    using BusWorker.Handlers;
    using BusWorker.HostServices;
    using ExternalServices;
    using Infrastructure.ServiceBus;
    using Infrastructure.WebApiClient.ExternalServices;
    using Infrastructure.WebApiClient.Resiliency;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using Shared.ReadModels;
    using System;

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

            //services.Configure<Infrastructure.ServiceBus.ParticularServiceBusSettings>(context.Configuration.GetSection("ParticularServiceBusSettings"));
            services.Configure<Infrastructure.ServiceBus.MassTransitSetting>(context.Configuration.GetSection("MassTransitSetting"));
            //services.Configure<Infrastructure.ServiceBus.AzureServiceBusSettings>(context.Configuration.GetSection("AzureServiceBusSettings"));
            //services.Configure<Infrastructure.ServiceBus.RebusBusSettings>(context.Configuration.GetSection("RebusBusSettings"));

            // The HostService 
            // This is the Service entry point management
            //services.AddHostedService<ParticularService>();
            services.AddHostedService<MassTransitBusService>();
            //services.AddHostedService<AzureBusService>();
            //services.AddHostedService<RebusService>();


            // Register the Event handler
            services.AddScoped<IMessageEventHandler<IntegrationEventIssued>, AzureEventOccurredHandler>();

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
                    c.DefaultRequestHeaders.Add("Authorization", "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiIsImtpZCI6InN2YiJ9.eyJzdWIiOjEsImlhdCI6MTYwNDY1MzIzNiwiZXhwIjoxNjA0NzM5NjM2LCJlbWFpbCI6ImFkbWluQHV0dS5nbG9iYWwiLCJnaXZlbl9uYW1lIjoiQWRtaW4iLCJmYW1pbHlfbmFtZSI6IkFkbWluIiwicHJlZmVycmVkX3VzZXJuYW1lIjoiYWRtaW4iLCJyb2xlcyI6WyJBZG1pbiIsIk1hbmFnZXIiLCJNZW1iZXIiXX0.WV4wEQxyp7tFTFMO-udiVgMrWLx48bDIQ5eZNR85AcPU57GxszUgSkHlTQqAC4GVtGj53ZAyMKPBZn1qt_WCpYrF9DSX5qRVMgflx7e3ZBtzqrDfbINUZQOF5KnNH5pEKUehXG4kLVLz0q7XhtNIkBchmrOAYXIU-rX9lej4Zbc");
                })
                .AddResiliencyPolicies(context.Configuration);

            // Add health check                                                                                                                                                                                                                     │
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
                ServiceDescriptor.Singleton(typeof(IHostedService),
                    typeof(HealthCheckPublisherOptions).Assembly
                        .GetType(HealthCheckServiceAssembly)));

            services.AddSingleton<IHealthCheckPublisher, ReadinessLivenessPublisher>();
        }
    }
}
