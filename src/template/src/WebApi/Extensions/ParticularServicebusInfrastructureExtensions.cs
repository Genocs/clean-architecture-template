using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class ParticularServicebusInfrastructureExtensions
{
    public static IServiceCollection AddParticularServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Setup registration
        services.Configure<ParticularServiceBusSettings>(config.GetSection("ParticularServiceBusSettings"));

        // Add Particular NService Bus
        services.AddSingleton<IServiceBusClient, ParticularServiceBusClient>();

        return services;
    }
}
