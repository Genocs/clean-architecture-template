using Genocs.CleanArchitecture.Template.Infrastructure.RebusSB;
using Genocs.CleanArchitecture.Template.Worker.RebusSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class RebusServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<RebusBusSettings>(context.Configuration.GetSection(RebusBusSettings.Position));
        services.AddHostedService<RebusService>();
    }
}
