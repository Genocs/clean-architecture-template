using Genocs.CleanArchitecture.Template.Infrastructure.RebusSB;
using Genocs.CleanArchitecture.Template.Worker.RebusSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public static class RebusServiceBusConfigurator
{
    public static IServiceCollection ConfigureRebus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RebusBusSettings>(configuration.GetSection(RebusBusSettings.Position));
        services.AddHostedService<RebusHostedService>();
        return services;
    }
}
