namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Application.Services;
    using Infrastructure.ParticularServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ParticularServicebusInfrastructureExtensions
    {
        public static IServiceCollection AddParticularServiceBus(this IServiceCollection services, IConfiguration config)
        {
            // Add Rebus Service Bus 
            services.AddSingleton<IServiceBus, ParticularServiceBus>();

            // Setup registration
            services.Configure<ParticularServiceBusOptions>(config.GetSection("ParticularServiceBusSettings"));

            return services;
        }
    }
}