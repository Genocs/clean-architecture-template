using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;
using Genocs.CleanArchitecture.Template.Worker.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.ConfigServices;

public class ParticularServiceBusConfigurator
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.Configure<ParticularServiceBusSettings>(context.Configuration.GetSection("ParticularServiceBusSettings"));
        services.AddHostedService<ParticularService>();
    }
}
