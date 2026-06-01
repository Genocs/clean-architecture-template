using Genocs.CleanArchitecture.Template.Application.Services;
using Microsoft.Extensions.Options;
using NServiceBus.Features;
#if MongoDb
using MongoDB.Driver;
#endif

namespace Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;

public class NServiceServiceBusClient(IOptions<NServiceServiceBusSettings> settings) : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private readonly NServiceServiceBusSettings _settings = settings.Value ?? throw new NullReferenceException("settings.Value.cannot be null");
    private IEndpointInstance? _instance;

    private async Task Initialize()
    {
        if (_instance == null)
        {
            #region ConfigureMetrics and Monitoring
            // endpointConfiguration.SendFailedMessagesTo("error");
            // endpointConfiguration.AuditProcessedMessagesTo("audit");
            // endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
            // var metrics = endpointConfiguration.EnableMetrics();
            // metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));
            #endregion

            var endpointConfiguration = new EndpointConfiguration(_settings.EndpointName);

            #region Configure Transport with Rabbit

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>()
                                .UseConventionalRoutingTopology(QueueType.Classic)
                                .SetHeartbeatInterval(TimeSpan.FromSeconds(30))
                                .ConnectionString(_settings.TransportConnectionString);
            #endregion

#if MongoDb
            if (_settings.UsePersistence)
            {
                var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
                persistence.MongoClient(new MongoClient(_settings.PersistenceConnectionString));
                persistence.DatabaseName(_settings.PersistenceDatabase!);
                persistence.UseTransactions(false); // Set replicaset and enable it
            }
#else
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            //persistence.SagaStorageDirectory(".\");
            endpointConfiguration.DisableFeature<Sagas>();

#endif
#region Register commands

            // transport.Routing().RouteToEndpoint(typeof(MyCommand), "Sample.SimpleSender");

            #endregion

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

            _instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        }
    }

    public async Task PublishEventAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : Common.CQRS.Events.IEvent
    {
        await Initialize();
        await _instance.Publish(message, cancellationToken);
    }

    public async Task SendCommandAsync<T>(T command, CancellationToken cancellationToken = default)
        where T : Common.CQRS.Commands.ICommand
    {
        await Initialize();
        await _instance.Send(command, cancellationToken);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // _instance.Stop();
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_instance != null)
        {
            await _instance.Stop();
        }
    }
}