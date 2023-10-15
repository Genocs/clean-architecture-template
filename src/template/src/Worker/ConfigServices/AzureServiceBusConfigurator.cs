namespace Genocs.MicroserviceLight.Template.BusWorker.ConfigServices
{
    using BusWorker.HostedServices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class AzureServiceBusConfigurator
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<Infrastructure.ServiceBus.AzureServiceBusSettings>(context.Configuration.GetSection("AzureServiceBusSettings"));
            services.AddHostedService<AzureBusHostService>();
        }
    }
}
