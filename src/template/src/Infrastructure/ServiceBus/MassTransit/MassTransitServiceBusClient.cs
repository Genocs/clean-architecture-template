using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;

public class MassTransitServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private readonly MassTransitSetting? _settings;

    private IPublishEndpoint? _publishEndpoint;

    public MassTransitServiceBusClient(IOptions<MassTransitSetting> settings)
    {
        _settings = settings.Value;

        if (_settings is null)
        {
            throw new NullReferenceException("settings.Value.cannot be null");
        }

        //ServiceBusConnectionStringBuilder connectionStringBuilder = new ServiceBusConnectionStringBuilder
        //{
        //    Endpoint = _settings.QueueEndpoint,
        //    EntityPath = _settings.QueueName,
        //    SasKeyName = _settings.QueueAccessPolicyName,
        //    SasKey = _settings.QueueAccessPolicyKey,
        //    TransportType = TransportType.Amqp
        //};

        //_queueClient = new QueueClient(connectionStringBuilder)
        //{
        //    PrefetchCount = _settings.PrefetchCount
        //};
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

        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        //if (_bus is not null)
        //{
        //    await _bus.DisposeAsync();
        //}

        //_bus = null;

        await Task.CompletedTask;
    }

    public async Task PublishEventAsync<T>(T @event)
        where T : Contracts.Interfaces.IEvent
    {
        await Task.CompletedTask;
    }

    public async Task SendCommandAsync<T>(T command)
        where T : Contracts.Interfaces.ICommand
    {
        await Task.CompletedTask;
    }
}
