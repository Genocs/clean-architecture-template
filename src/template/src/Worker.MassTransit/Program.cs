using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;
using Genocs.CleanArchitecture.Template.Worker.MassTransit.Handlers;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Worker.MassTransit;

public class Program
{
    public static void Main(string[] args)
    {
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

                        //cfg.HealthCheck(context);
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
                //services.AddMassTransitHostedService();
            });
}
