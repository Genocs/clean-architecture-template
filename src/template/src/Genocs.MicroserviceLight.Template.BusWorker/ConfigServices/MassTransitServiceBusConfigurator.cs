namespace Genocs.MicroserviceLight.Template.BusWorker.ConfigServices
{
    using BusWorker.HostedServices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class MassTransitServiceBusConfigurator
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<Infrastructure.ServiceBus.MassTransitSetting>(context.Configuration.GetSection("MassTransitSetting"));
            services.AddHostedService<MassTransitBusService>();
        }
    }
}
