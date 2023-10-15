namespace Genocs.MicroserviceLight.Template.BusWorker.ConfigServices
{
    using BusWorker.HostedServices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class ParticularServiceBusConfigurator
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<Infrastructure.ServiceBus.ParticularServiceBusSettings>(context.Configuration.GetSection("ParticularServiceBusSettings"));
            services.AddHostedService<ParticularService>();
        }
    }
}
