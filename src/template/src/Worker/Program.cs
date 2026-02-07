using Genocs.CleanArchitecture.Template.Worker.Configurator;
using Genocs.CleanArchitecture.Template.Worker.HealthCheck;
using Genocs.CleanArchitecture.Template.Worker.WebApi;
using Genocs.Core.Builders;
using Genocs.Logging;
using Genocs.Tracing;
using Serilog;

StaticLogger.EnsureInitialized();

IHost host = Host.CreateDefaultBuilder(args)
    .UseLogging()
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddGenocs(hostContext.Configuration)
            .AddOpenTelemetry()
            .Build();

#if MassTransit
        services.ConfigureMassTransit(hostContext.Configuration);
#endif
#if NServiceBus
        services.ConfigureNServiceBus(hostContext.Configuration);
#endif
#if Rebus
        services.ConfigureRebus(hostContext.Configuration);
#endif

        services.ConfigureWebApiServices(hostContext.Configuration);
        services.ConfigureHealthChecks(hostContext.Configuration);
    })
    .Build();

await host.RunAsync();

await Log.CloseAndFlushAsync();
