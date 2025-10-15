using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB.HostedServices;

internal class ParticularService : IHostedService
{

    private readonly ILogger<ParticularService> _logger;
    private readonly EndpointConfiguration _configuration;

    private IEndpointInstance _instance;

    public ParticularService(IOptions<NServiceServiceBusSettings> settings, ILogger<ParticularService> logger)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(settings.Value);

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Start NServiceBus configuration
        #region ConfigureLicense
        #endregion

        _configuration = new EndpointConfiguration(settings.Value.EndpointName);

        // https://docs.particular.net/nservicebus/serialization/
        _configuration.UseSerialization<SystemJsonSerializer>();

        _logger.LogInformation($"Start endpoint name: '{settings.Value.EndpointName}'");

        #region Configure Transport with Rabbit

        var transport = _configuration.UseTransport<RabbitMQTransport>()
                                        .UseConventionalRoutingTopology(QueueType.Classic)
                                        .SetHeartbeatInterval(TimeSpan.FromSeconds(30))
                                        .ConnectionString(settings.Value.TransportConnectionString);
        #endregion

        #region Register commands

        // transport.Routing().RouteToEndpoint(typeof(MyCommand), "Sample.SimpleSender");

        #endregion

        #region ConfigureMetrics and Monitoring

        // _configuration.SendFailedMessagesTo("error");
        // _configuration.AuditProcessedMessagesTo("audit");
        // _configuration.SendHeartbeatTo("Particular.ServiceControl");
        // var metrics = _configuration.EnableMetrics();
        // metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));
        #endregion

        #region Configure Persistance with MongoDb

        var persistence = _configuration.UsePersistence<MongoPersistence>();
        persistence.MongoClient(new MongoClient(settings.Value.PersistenceConnectionString));
        persistence.DatabaseName(settings.Value.PersistenceDatabase);
        persistence.UseTransactions(false); // Set replicaset and enable it
        #endregion

        // Unobtrusive mode.
        // var conventions = _configuration.Conventions();
        // conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events");

        _configuration.EnableInstallers();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        _instance = await Endpoint.Start(_configuration, cancellationToken);
        _logger.LogInformation("Started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        await _instance.Stop(cancellationToken);
        _logger.LogInformation("Stopped");
    }
}
