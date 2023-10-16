using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
using Genocs.CleanArchitecture.Template.Shared.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

internal class AzureBusService : IHostedService
{
    private readonly ILogger<AzureBusService> _logger;
    private readonly AzureServiceBusSettings _settings;

    private readonly Func<AzureServiceBusSettings, IQueueClient> _createQueueClient;

    private readonly IServiceProvider _serviceProvider;

    private IQueueClient _busClient;
    private Dictionary<string, KeyValuePair<Type, Type>> _handlers = new Dictionary<string, KeyValuePair<Type, Type>>();

    public AzureBusService(IOptions<AzureServiceBusSettings> options, ILogger<AzureBusService> logger, IServiceProvider serviceProvider)
        : this(options, logger, CreateQueueClient, serviceProvider)
    {
    }

    public AzureBusService(
                           IOptions<AzureServiceBusSettings> options,
                           ILogger<AzureBusService> logger,
                           Func<AzureServiceBusSettings, IQueueClient> createQueueClient,
                           IServiceProvider serviceProvider)
    {
        _settings = options.Value;

        if (_settings == null)
        {
            throw new NullReferenceException("options cannot be null");
        }

        _serviceProvider = serviceProvider;

        _logger = logger;
        _createQueueClient = createQueueClient;
    }

    protected void RegisterMessage<T, TH>()
        where T : IIntegrationEvent
        where TH : IMessageEventHandler<T>
    {
        string eventName = typeof(T).Name;
        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new KeyValuePair<Type, Type>(typeof(T), typeof(TH)));
        }
    }

    private static IQueueClient CreateQueueClient(AzureServiceBusSettings options)
    {
        ServiceBusConnectionStringBuilder connectionStringBuilder = new ServiceBusConnectionStringBuilder
        {
            Endpoint = options.QueueEndpoint,
            EntityPath = options.QueueName,
            SasKeyName = options.QueueAccessPolicyName,
            SasKey = options.QueueAccessPolicyKey,
            TransportType = TransportType.Amqp
        };

        return new QueueClient(connectionStringBuilder)
        {
            PrefetchCount = options.PrefetchCount
        };
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        _busClient = _createQueueClient(_settings);

        _busClient.RegisterMessageHandler(
            ProcessMessageAsync,
            new MessageHandlerOptions(ProcessMessageExceptionAsync)
            {
                AutoComplete = false,
                MaxConcurrentCalls = _settings.MaxConcurrency
            });

        _logger.LogInformation("Started");
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        await _busClient.CloseAsync();

        _logger.LogInformation("Stopped");
    }

    private async Task ProcessMessageAsync(Message message, CancellationToken ct)
    {
        _logger.LogInformation("Processing message {messageId}", message.MessageId);

        string eventName = $"{message.Label}";
        if (_handlers.ContainsKey(eventName) && _serviceProvider != null)
        {
            //using (var scope = _services.CreateScope())
            //{
            var type = _handlers[eventName];
            if (type.Key != null && type.Value != null)
            {
                var handler = _serviceProvider.GetService(type.Value);
                if (handler != null)
                {
                    var evt = TryGenericMessage(message, type.Key);
                    if (evt is not null)
                    {
                        var concreteType = typeof(IMessageEventHandler<>).MakeGenericType(type.Key);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { evt });
                        await _busClient.CompleteAsync(message.SystemProperties.LockToken); // Send the ack 
                        _logger.LogInformation("Processed message {messageId}", message.MessageId);
                        return;
                    }
                }
            }
            //}
        }
        else
        {
            _logger.LogError("handlers do not contains data for message with label: '{Label}', messageId: {MessageId}", message.Label, message.MessageId);
        }

        try
        {
            await _busClient.DeadLetterAsync(message.SystemProperties.LockToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error moving message {messageId} to dead letter queue", message.MessageId);
        }
    }

    private Task ProcessMessageExceptionAsync(ExceptionReceivedEventArgs exceptionEvent)
    {
        _logger.LogError(exceptionEvent.Exception, "Exception processing message");

        return Task.CompletedTask;
    }

    private object? TryGenericMessage(Message incomingMessage, Type type)
    {
        try
        {
            if (incomingMessage.Body != null && incomingMessage.Body.Length > 0)
            {
                using MemoryStream payloadStream = new(incomingMessage.Body, false);
                using StreamReader streamReader = new(payloadStream, Encoding.UTF8);
                return JsonConvert.DeserializeObject(streamReader.ReadToEnd(), type);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot parse payload from message {messageId}", incomingMessage.MessageId);
        }
        return null;
    }
}
