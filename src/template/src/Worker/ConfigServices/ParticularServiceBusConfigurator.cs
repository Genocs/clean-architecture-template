using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;
using Genocs.CleanArchitecture.Template.Worker.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.ConfigServices;

public class ParticularServiceBusConfigurator
{
    public static void ConfigureNServiceServiceBusServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<NServiceServiceBusSettings>(context.Configuration.GetSection(NServiceServiceBusSettings.Position));
        services.AddHostedService<ParticularService>();
    }
}
