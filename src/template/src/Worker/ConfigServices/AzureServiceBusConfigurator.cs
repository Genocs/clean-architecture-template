using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
using Genocs.CleanArchitecture.Template.Worker.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.ConfigServices;


public class AzureServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<AzureServiceBusSettings>(context.Configuration.GetSection("AzureServiceBusSettings"));
        services.AddHostedService<AzureBusHostService>();
    }
}
