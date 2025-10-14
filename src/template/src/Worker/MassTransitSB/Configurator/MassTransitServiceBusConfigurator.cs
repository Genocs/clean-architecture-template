using Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;
using Genocs.CleanArchitecture.Template.Worker.ParticularSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class MassTransitServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<MassTransitSetting>(context.Configuration.GetSection(MassTransitSetting.Position));
        services.AddHostedService<MassTransitBusService>();
    }
}
