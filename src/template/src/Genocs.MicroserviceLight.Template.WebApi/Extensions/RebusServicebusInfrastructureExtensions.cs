namespace Genocs.MicroserviceLight.Template.WebApi.Extensions;

using Application.Services;
using Infrastructure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class RebusServicebusInfrastructureExtensions
{
    public static IServiceCollection AddRebusServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Add Rebus Service Bus 
        services.AddSingleton<IServiceBusClient, RebusServiceBusClient>();

        // Setup registration
        services.Configure<RebusBusSettings>(config.GetSection("RebusBusSettings"));

        return services;
    }
}
