using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

internal class ParticularService : IHostedService
{

    private readonly ILogger<ParticularService> _logger;
    private readonly EndpointConfiguration _configuration;

    private IEndpointInstance _instance;

    public ParticularService(IOptions<ParticularServiceBusSettings> settings, ILogger<ParticularService> logger)
    {
        if (settings is null)
        {
            throw new ArgumentNullException(nameof(settings));
        }

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Start NServiceBus configuration
        #region ConfigureLicense
        #endregion

        #region ConfigureMetrics and Monitoring

        // endpointConfiguration.SendFailedMessagesTo("error");
        // endpointConfiguration.AuditProcessedMessagesTo("audit");
        // endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
        // var metrics = endpointConfiguration.EnableMetrics();
        // metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));
        #endregion

        _configuration = new EndpointConfiguration(settings.Value.EndpointName);
        _logger.LogInformation($"Start endpoint name: '{settings.Value.EndpointName}'");

        #region Configure Transport with Rabbit
        var transport = _configuration.UseTransport<RabbitMQTransport>();
//        transport.UseConventionalRoutingTopology();
        transport.ConnectionString(settings.Value.TransportConnectionString);
        _logger.LogInformation($"Transport connection string: '{settings.Value.TransportConnectionString}'");
        #endregion

        #region Configure Persistance with MongoDb

        var persistence = _configuration.UsePersistence<MongoPersistence>();
        persistence.MongoClient(new MongoClient(settings.Value.PersistenceConnectionString));
        persistence.DatabaseName(settings.Value.PersistenceDatabase);
        persistence.UseTransactions(true); // Set replicaset and enable it
        #endregion

        #region Register commands

        // transport.Routing().RouteToEndpoint(typeof(MyCommand), "Sample.SimpleSender");

        #endregion

        // Unobtrusive mode.
        var conventions = _configuration.Conventions();

        conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events");

        //conventions.DefiningEventsAs(type =>
        //    type.Namespace == "Genocs.CleanArchitecture.Template.Shared.Events"
        //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
        //);

        // https://docs.particular.net/nservicebus/serialization/
        _configuration.UseSerialization<NewtonsoftJsonSerializer>();

        _configuration.EnableInstallers();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        _instance = await Endpoint.Start(_configuration);
        _logger.LogInformation("Started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        await _instance.Stop();
        _logger.LogInformation("Stopped");
    }
}
