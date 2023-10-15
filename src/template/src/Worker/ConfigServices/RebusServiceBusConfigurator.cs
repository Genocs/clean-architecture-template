using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Rebus;
using Genocs.CleanArchitecture.Template.Worker.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.ConfigServices;

public class RebusServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<RebusBusSettings>(context.Configuration.GetSection("RebusBusSettings"));
        services.AddHostedService<RebusService>();
    }
}
