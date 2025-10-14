using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;
using Genocs.CleanArchitecture.Template.Worker.ParticularSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public class ParticularServiceBusConfigurator
{
    public static void ConfigureNServiceServiceBusServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<NServiceServiceBusSettings>(context.Configuration.GetSection(NServiceServiceBusSettings.Position));
        services.AddHostedService<ParticularService>();
    }
}
