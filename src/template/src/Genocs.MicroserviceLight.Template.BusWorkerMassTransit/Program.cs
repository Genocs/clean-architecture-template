namespace Genocs.MicroserviceLight.Template.BusWorkerMassTransit
{
    using BusWorkerMassTransit.Handlers;
    using Infrastructure.ServiceBus;
    using MassTransit;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Genocs.MicroserviceLight.Template Bus is starting.");

            await host.RunAsync();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    MassTransitSetting settings = new();
                    hostContext.Configuration.GetSection("MassTransitSetting").Bind(settings);
                    services.AddSingleton(settings);

                    services.AddMassTransit(x =>
                    {
                        //x.AddConsumersFromNamespaceContaining<MerchantStatusChangedConsumer>();

                        x.AddConsumer<DemoEventOccurredHandler>()
                                .Endpoint(x =>
                                {
                                    x.ConcurrentMessageLimit = 5;
                                    x.PrefetchCount = 5;
                                });

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            //cfg.ReceiveEndpoint("merchantstatus", e =>
                            //{
                            //    e.PrefetchCount = 5;
                            //    e.ConcurrentMessageLimit = 5;
                            //    //e.UseMessageRetry(r => r.);
                            //    e.Consumer<MerchantStatusChangedConsumer>(context);
                            //});

                            cfg.UseHealthCheck(context);
                            cfg.ConfigureEndpoints(context);
                            cfg.Host(settings.HostName, settings.VirtualHost,
                                h =>
                                {
                                    h.Username(settings.UserName);
                                    h.Password(settings.Password);
                                }
                            );
                        });
                    });
                    services.AddMassTransitHostedService();
                });
    }
}
