using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
using Genocs.CleanArchitecture.Template.Worker.AzureSB.HostService;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class AzureServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<AzureServiceBusSettings>(context.Configuration.GetSection(AzureServiceBusSettings.Position));
        services.AddHostedService<AzureBusHostService>();
    }
}
