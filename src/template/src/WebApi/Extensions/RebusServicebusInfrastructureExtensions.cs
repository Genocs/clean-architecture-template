using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Rebus;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class RebusServicebusInfrastructureExtensions
{
    public static IServiceCollection AddRebusServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Configure Rebus settings
        services.Configure<RebusBusSettings>(config.GetSection(RebusBusSettings.Position));

        // Add Rebus Service Bus
        services.AddSingleton<IServiceBusClient, RebusServiceBusClient>();

        return services;
    }
}
