using Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;
using Genocs.CleanArchitecture.Template.Worker.MassTransitSB.Handlers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Genocs.CleanArchitecture.Template.Worker.Configurator;

public static class MassTransitServiceBusConfigurator
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {

        MassTransitSettings settings = new();
        configuration.GetSection(MassTransitSettings.Position).Bind(settings);
        services.AddSingleton(settings);

        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(x =>
        {
            // Consumer configuration
            //x.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();
            x.AddConsumersFromNamespaceContaining<RegistrationCompletedHandler>();

            // Set the transport
            //x.UsingRabbitMq(ConfigureBus);

            //x.AddConsumer<RegistrationCompletedHandler>()
            //        .Endpoint(x =>
            //        {
            //            x.ConcurrentMessageLimit = 5;
            //            x.PrefetchCount = 5;
            //        });

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

        return services;
    }
    static void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
    {
        // configurator.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1", "attachments"));

        //configurator.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Consumer<RoutingSlipBatchEventConsumer>(), e =>
        //{
        //    e.PrefetchCount = 20;

        //    e.Batch<RoutingSlipCompleted>(b =>
        //    {
        //        b.MessageLimit = 10;
        //        b.TimeLimit = TimeSpan.FromSeconds(5);

        //        b.Consumer<RoutingSlipBatchEventConsumer, RoutingSlipCompleted>(context);
        //    });
        //});

        // This configuration allow to handle the Scheduling
        configurator.UseMessageScheduler(new Uri("queue:quartz"));

        // This configuration will configure the Activity Definition
        configurator.ConfigureEndpoints(context);
    }
}
