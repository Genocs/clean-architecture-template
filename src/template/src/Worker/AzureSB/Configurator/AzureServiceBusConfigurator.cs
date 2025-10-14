using Genocs.CleanArchitecture.Template.Infrastructure.AzureSB;
using Genocs.CleanArchitecture.Template.Worker.AzureSB.Handlers;
using Genocs.CleanArchitecture.Template.Worker.AzureSB.HostService;
using Genocs.CleanArchitecture.Template.Contracts.Events;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class AzureServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<AzureServiceBusSettings>(context.Configuration.GetSection(AzureServiceBusSettings.Position));
        services.AddHostedService<AzureBusHostService>();

        // Register the Event handler
        services.AddScoped<IMessageEventHandler<IntegrationEventIssued>, AzureEventOccurredHandler>();
    }
}
