namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Application.Services;
    using Infrastructure.AzureServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AzureServicebusInfrastructureExtensions
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, IConfiguration config)
        {
            // Add Azure Service Bus 
            services.AddSingleton<IServiceBusClient, AzureServiceBusClient>();

            // Setup registration
            services.Configure<AzureServiceBusOptions>(config.GetSection("AzureServiceBusSettings"));

            return services;
        }
    }
}