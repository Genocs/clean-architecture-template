namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Application.Services;
    using Infrastructure.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class MassTransitInfrastructureExtensions
    {
        public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services, IConfiguration config)
        {
            // Add Mass Transit Azure Service Bus 
            services.AddSingleton<IServiceBusClient, MassTransitServiceBusClient>();

            // Settings registration
            services.Configure<MassTransitSetting>(config.GetSection("MassTransitSettings"));

            return services;
        }
    }
}