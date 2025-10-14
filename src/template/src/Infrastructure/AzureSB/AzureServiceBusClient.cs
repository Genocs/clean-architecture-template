using System.Text;
using Genocs.CleanArchitecture.Template.Application.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Genocs.CleanArchitecture.Template.Infrastructure.AzureSB;

public class AzureServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private readonly AzureServiceBusSettings _settings;

    private IQueueClient _queueClient;

    public AzureServiceBusClient(IOptions<AzureServiceBusSettings> settings)
    {
        _settings = settings.Value;

        if (_settings is null)
        {
            throw new NullReferenceException("settings.Value.cannot be null");
        }

        var connectionStringBuilder = new ServiceBusConnectionStringBuilder
        {
            Endpoint = _settings.QueueEndpoint,
            EntityPath = _settings.QueueName,
            SasKeyName = _settings.QueueAccessPolicyName,
            SasKey = _settings.QueueAccessPolicyKey,
            TransportType = TransportType.Amqp
        };

        _queueClient = new QueueClient(connectionStringBuilder)
        {
            PrefetchCount = _settings.PrefetchCount
        };
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
        /*
        if (_bus is not null)
        {
            await _bus.DisposeAsync();
        }
        _bus = null;
        */

        await Task.CompletedTask;
    }

    public async Task PublishEventAsync<T>(T @event)
        where T : Contracts.Interfaces.IEvent
    {
        var msg = new Message();
        string strMsg = JsonConvert.SerializeObject(@event);

        /*
         * Please evaluate how ho implement a more efficient way
         * to serialize the event to a byte
         */

        /*
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, evt);
            msg.Body = ms.ToArray();
        }
        */

        msg.Body = Encoding.ASCII.GetBytes(strMsg);
        await _queueClient.SendAsync(msg);
    }

    public async Task SendCommandAsync<T>(T command)
        where T : Contracts.Interfaces.ICommand
    {
        var msg = new Message();
        string strMsg = JsonConvert.SerializeObject(command);

        /*
         * Please evaluate how ho implement a more efficient way
         * to serialize the event to a byte
         * 
         */

        /*
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, evt);
            msg.Body = ms.ToArray();
        }
        */

        msg.Body = Encoding.ASCII.GetBytes(strMsg);
        await _queueClient.SendAsync(msg);
    }
}
