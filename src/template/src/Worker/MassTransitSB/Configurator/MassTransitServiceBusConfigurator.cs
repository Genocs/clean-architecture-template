using Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;
using Genocs.CleanArchitecture.Template.Worker.MassTransitSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class MassTransitServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<MassTransitSettings>(context.Configuration.GetSection(MassTransitSettings.Position));
        services.AddHostedService<MassTransitBusService>();
    }
}
