using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;
using Genocs.CleanArchitecture.Template.Worker.ParticularSB.HostedServices;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public static class ParticularConfigurator
{
    public static IServiceCollection ConfigureNServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NServiceServiceBusSettings>(configuration.GetSection(NServiceServiceBusSettings.Position));
        services.AddHostedService<ParticularHostedService>();

        return services;
    }
}
