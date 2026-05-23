using Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB.HostedServices;

internal class ParticularHostedService : IHostedService
{
    private readonly ILogger<ParticularHostedService> _logger;
    private readonly EndpointConfiguration _configuration;

    private IEndpointInstance? _instance;

    public ParticularHostedService(IOptions<NServiceServiceBusSettings> settings, ILogger<ParticularHostedService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(settings.Value);

        _logger = logger;
        NServiceServiceBusSettings options = settings.Value;

        _configuration = new EndpointConfiguration(options.EndpointName);

        _configuration.UseSerialization<SystemJsonSerializer>();
        _configuration.EnableInstallers();

        _logger.LogInformation($"Start endpoint name: '{options.EndpointName}'");

        #region Configure Transport with Rabbit

        _ = _configuration.UseTransport<RabbitMQTransport>()
                                        .UseConventionalRoutingTopology(QueueType.Classic)
                                        .SetHeartbeatInterval(TimeSpan.FromSeconds(30))
                                        .ConnectionString(options.TransportConnectionString);
        #endregion

        #region Register commands

        // transport.Routing().RouteToEndpoint(typeof(MyCommand), "Sample.SimpleSender");

        #endregion

#if MongoDb
        if (settings.Value.UsePersistence)
        {
            var persistence = _configuration.UsePersistence<MongoPersistence>();
            persistence.MongoClient(new MongoClient(settings.Value.PersistenceConnectionString));
            persistence.DatabaseName(settings.Value.PersistenceDatabase!);
            persistence.UseTransactions(false); // Set replicaset and enable it
        }
#else
        var persistence = _configuration.UsePersistence<LearningPersistence>();
        //persistence.SagaStorageDirectory(".\");
        _configuration.DisableFeature<NServiceBus.Features.Sagas>();
#endif

        // Unobtrusive mode.
        // var conventions = _configuration.Conventions();
        // conventions.DefiningEventsAs(type => type.Namespace == "Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events");

        _configuration.EnableInstallers();

        #region ConfigureMetrics and Monitoring

        // _configuration.SendFailedMessagesTo("error");
        // _configuration.AuditProcessedMessagesTo("audit");
        // _configuration.SendHeartbeatTo("Particular.ServiceControl");
        // var metrics = _configuration.EnableMetrics();
        // metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));
        #endregion
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
        if (_instance is not null)
        {
            await _instance.Stop(cancellationToken);
        }

        _logger.LogInformation("Stopped");
    }
}
