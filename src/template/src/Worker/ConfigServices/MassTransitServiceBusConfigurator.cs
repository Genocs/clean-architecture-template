using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;
using Genocs.CleanArchitecture.Template.Worker.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.ConfigServices;

public class MassTransitServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<MassTransitSetting>(context.Configuration.GetSection("MassTransitSetting"));
        services.AddHostedService<MassTransitBusService>();
    }
}
