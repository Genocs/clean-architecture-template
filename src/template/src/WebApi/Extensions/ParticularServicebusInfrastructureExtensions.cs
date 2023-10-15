namespace Genocs.MicroserviceLight.Template.WebApi.Extensions;

using Application.Services;
using Infrastructure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ParticularServicebusInfrastructureExtensions
{
    public static IServiceCollection AddParticularServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Add Particular NService Bus 
        services.AddSingleton<IServiceBusClient, ParticularServiceBusClient>();

        // Setup registration
        services.Configure<ParticularServiceBusSettings>(config.GetSection("ParticularServiceBusSettings"));

        return services;
    }
}
