using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class NServiceBusInfrastructureExtensions
{
    public static IServiceCollection AddNServiceBusServiceBus(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<NServiceServiceBusSettings>(config.GetSection(NServiceServiceBusSettings.Position));

        services.AddSingleton<IServiceBusClient, NServiceServiceBusClient>();

        return services;
    }
}
