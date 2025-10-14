using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class MassTransitInfrastructureExtensions
{
    public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Settings registration
        services.Configure<MassTransitSetting>(config.GetSection(MassTransitSetting.Position));

        // Add Mass Transit Azure Service Bus
        services.AddSingleton<IServiceBusClient, MassTransitServiceBusClient>();

        return services;
    }
}
