namespace Genocs.MicroserviceLight.Template.BusWorker.ConfigServices
{
    using BusWorker.HostedServices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class RebusServiceBusConfigurator
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<Infrastructure.ServiceBus.RebusBusSettings>(context.Configuration.GetSection("RebusBusSettings"));
            services.AddHostedService<RebusService>();
        }
    }
}
