using Genocs.CleanArchitecture.Template.Contracts.Events;
using Genocs.CleanArchitecture.Template.Worker.ParticularSB;
using Genocs.CleanArchitecture.Template.Worker.ParticularSB.ExternalServices;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Worker;

internal static class NServiceBusHostBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.UseConsoleLifetime();

        builder.UseMicrosoftLogFactoryLogging();

        SetUpNServiceBusBasic(builder);

        // Setup with RabbitMQ and MongoDB
        // SetUpNServiceBus(builder);

        builder.ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<BackgroundWorker>();
            services.AddHostedService<TimedHostedService>();

            services.AddSingleton<ICalculateStuff, CalculateStuff>();

        });

        return builder;
    }

    public static IHostBuilder SetUpNServiceBusBasic(IHostBuilder builder)
    {
        builder.UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("Sample.BackEnd");
            endpointConfiguration.SendOnly();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory(".");
            transport.Routing().RouteToEndpoint(typeof(RegistrationCompleted), "Sample.RegistrationCompleted");

            // Unobtrusive mode.
            //var conventions = endpointConfiguration.Conventions();
            //conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events");

            //conventions.DefiningEventsAs(type =>
            //    type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events"
            //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
            //);

            // https://docs.particular.net/nservicebus/serialization/
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

        return builder;
    }

    public static IHostBuilder SetUpNServiceBus(IHostBuilder builder)
    {
        builder.UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("Sample.FrontEnd");

            // endpointConfiguration.SendOnly();

            // Use Rabbit as transport
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                                            .UseConventionalRoutingTopology(QueueType.Classic)
                                            .SetHeartbeatInterval(TimeSpan.FromSeconds(30))
                                            .ConnectionString("amqp://rabbitmq");

            // Use MongoDB as persistence layer
            // Save all the data related to Saga and so on to MongoDB
            var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
            persistence.MongoClient(new MongoClient("mongodb://localhost:27017"));
            persistence.DatabaseName("DemoDB");
            persistence.UseTransactions(false);

            // Unobtrusive mode.
            // var conventions = endpointConfiguration.Conventions();
            // conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events");

            // conventions.DefiningEventsAs(type =>
            //    type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events"
            //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
            // );

            // https://docs.particular.net/nservicebus/serialization/
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

        return builder;
    }
}
