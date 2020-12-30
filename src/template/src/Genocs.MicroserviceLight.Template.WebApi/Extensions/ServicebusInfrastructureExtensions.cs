namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Application.Services;
    using Genocs.MicroserviceLight.Template.Infrastructure.RebusServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicebusInfrastructureExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration config)
        {
            // Add Rebus Service Bus 
            services.AddScoped<IServiceBus, RebusServiceBus>();

            // Setup registration
            services.Configure<RebusBusOptions>(config.GetSection("RebusBusSettings"));

            return services;
        }
    }
}