using Genocs.CleanArchitecture.Template.Worker.Particular.Messages;
using Genocs.CleanArchitecture.Template.Worker.Particular.Services;
using MongoDB.Driver;
using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

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
                services.AddHostedService<Worker>();
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
            transport.Routing().RouteToEndpoint(typeof(DemoMessage), "Sample.BackEnd");


            // Unobtrusive mode. 
            //var conventions = endpointConfiguration.Conventions();
            //conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events");

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
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            // transport.UseConventionalRoutingTopology();
            transport.ConnectionString("amqp://rabbitmq");

            // Use MongoDB as persistence layer
            // Save all the data related to Saga and so on to MongoDB
            var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
            persistence.MongoClient(new MongoClient("mongodb://localhost:27017"));
            persistence.DatabaseName("UTU_Platform_DemoDB");
            persistence.UseTransactions(false);

            // Unobtrusive mode.
            // var conventions = endpointConfiguration.Conventions();
            // conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events");

            // conventions.DefiningEventsAs(type =>
            //    type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events"
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
