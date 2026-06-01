using Genocs.CleanArchitecture.Template.Contracts.Events;
using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB;

public static class ParticularHostBuilder
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        NServiceServiceBusSettings settings = new();
        configuration.GetSection(NServiceServiceBusSettings.Position).Bind(settings);
        services.AddSingleton(settings);

        return services;
    }

    public static IHostBuilder ConfigureNServiceBusDemo(this IHostBuilder builder)
    {
        builder.UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("Genocs.CleanArchitecture.Template");
            endpointConfiguration.SendOnly();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory(".");
            transport.Routing().RouteToEndpoint(typeof(RegistrationCompleted), "Sample.RegistrationCompleted");

            // Unobtrusive mode.
            // var conventions = endpointConfiguration.Conventions();
            // conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events");

            // conventions.DefiningEventsAs(type =>
            //     type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events"
            //     || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
            // );

            // https://docs.particular.net/nservicebus/serialization/
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

        return builder;
    }

    public static IHostBuilder ConfigureNServiceBus(this IHostBuilder builder)
    {

        builder.UseNServiceBus(context =>
        {
            // GetAsync the configuration
            NServiceServiceBusSettings settings = new();
            context.Configuration.GetSection(NServiceServiceBusSettings.Position).Bind(settings);

            var endpointConfiguration = new EndpointConfiguration(settings.EndpointName);

            // endpointConfiguration.SendOnly();

            // Use Rabbit as transport
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                                            .UseConventionalRoutingTopology(QueueType.Classic)
                                            .SetHeartbeatInterval(TimeSpan.FromSeconds(30))
                                            .ConnectionString(settings.TransportConnectionString);

            // Setup persistence layer
            if (settings.UsePersistence)
            {
                // Save all the data related to Saga and so on to MongoDB
                // var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
                // persistence.MongoClient(new MongoClient(settings.PersistenceConnectionString));
                // persistence.DatabaseName(settings.PersistenceDatabase!);
                // persistence.UseTransactions(false);
            }

            // Unobtrusive mode.

            /*
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(type =>
                type.Namespace != null &&
                (type.Namespace.StartsWith("Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events")
                 || type.Namespace.StartsWith("Genocs.CleanArchitecture.Template.ContractsNServiceBus.TransactionSaga")));

            conventions.DefiningCommandsAs(type =>
                type.Namespace != null &&
                type.Namespace.StartsWith("Genocs.CleanArchitecture.Template.ContractsNServiceBus.Commands"));
            */

            // https://docs.particular.net/nservicebus/serialization/
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

        return builder;
    }
}
