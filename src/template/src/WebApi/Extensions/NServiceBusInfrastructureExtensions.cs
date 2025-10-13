using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class NServiceBusInfrastructureExtensions
{
    public static IServiceCollection AddNServiceBusServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Setup registration
        services.Configure<NServiceServiceBusSettings>(config.GetSection("NServiceBusSettings"));

        // Add Particular NService Bus
        services.AddSingleton<IServiceBusClient, NServiceServiceBusClient>();

        return services;
    }
}
