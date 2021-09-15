namespace Genocs.MicroserviceLight.Template.BusWorkerParticular
{
    using BusWorkerParticular.Messages;
    using BusWorkerParticular.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;
    using NServiceBus;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Genocs.MicroserviceLight.Template Bus is starting.");

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            builder.UseConsoleLifetime();

            builder.UseMicrosoftLogFactoryLogging();


            builder.ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddHostedService<TimedHostedService>();

                    services.AddSingleton<ICalculateStuff, CalculateStuff>();

                });

            SetUpNServiceBusBasic(builder);

            // Setup with RabbitMQ and MongoDB
            //SetUpNServiceBus(builder);

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

                endpointConfiguration.RegisterComponents(
                    registration: configureComponents =>
                    {
                        configureComponents.ConfigureComponent<CalculateStuff>(DependencyLifecycle.SingleInstance);
                    });


                // Unobtrusive mode. 
                //var conventions = endpointConfiguration.Conventions();
                //conventions.DefiningEventsAs(type => type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events");

                //conventions.DefiningEventsAs(type =>
                //    type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events"
                //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
                //);

                // https://docs.particular.net/nservicebus/serialization/
                endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
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
                //endpointConfiguration.SendOnly();

                // Use Rabbit as transport
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString("amqp://rabbitmq");

                // Use MongoDB as persistance layer
                // Save all the data related to Saga and so on to MongoDB
                var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
                persistence.MongoClient(new MongoClient("mongodb://localhost:27017"));
                persistence.DatabaseName("UTU_Platform_DemoDB");
                persistence.UseTransactions(false);

                // Unobtrusive mode. 
                //var conventions = endpointConfiguration.Conventions();
                //conventions.DefiningEventsAs(type => type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events");

                //conventions.DefiningEventsAs(type =>
                //    type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events"
                //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
                //);

                // https://docs.particular.net/nservicebus/serialization/
                endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
                endpointConfiguration.EnableInstallers();


                return endpointConfiguration;
            });

            return builder;
        }
    }
}
