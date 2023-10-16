using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class AzureServicebusInfrastructureExtensions
{
    public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Add Azure Service Bus
        services.AddSingleton<IServiceBusClient, AzureServiceBusClient>();

        // Setup registration
        services.Configure<AzureServiceBusSettings>(config.GetSection("AzureServiceBusSettings"));

        return services;
    }
}
