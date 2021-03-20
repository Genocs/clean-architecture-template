namespace Genocs.MicroserviceLight.Template.BusHost.HostServices
{
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Shared.ReadModels;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AzureBusService : IHostedService
    {
        private readonly JsonSerializer _serializer;
        private readonly ILogger<AzureBusService> _logger;
        private readonly Infrastructure.ServiceBus.AzureServiceBusOptions _options;

        private readonly Func<Infrastructure.ServiceBus.AzureServiceBusOptions, IQueueClient> _createQueueClient;

        private IQueueClient _busClient;

        public AzureBusService(IOptions<Infrastructure.ServiceBus.AzureServiceBusOptions> options, ILogger<AzureBusService> logger)
            : this(options, logger, CreateQueueClient)
        { }

        public AzureBusService(IOptions<Infrastructure.ServiceBus.AzureServiceBusOptions> options, ILogger<AzureBusService> logger,
            Func<Infrastructure.ServiceBus.AzureServiceBusOptions, IQueueClient> createQueueClient)
        {
            _options = options.Value;

            if (_options == null)
            {
                throw new NullReferenceException("options cannot be null");
            }

            _logger = logger;
            _createQueueClient = createQueueClient;

            _serializer = new JsonSerializer();
        }

        private static IQueueClient CreateQueueClient(Infrastructure.ServiceBus.AzureServiceBusOptions options)
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
            _busClient = _createQueueClient(_options);

            _busClient.RegisterMessageHandler(
                ProcessMessageAsync,
                new MessageHandlerOptions(ProcessMessageExceptionAsync)
                {
                    AutoComplete = false,
                    MaxConcurrentCalls = _options.MaxConcurrency
                });

            _logger.LogInformation("Started");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            await _busClient?.CloseAsync();

            _logger.LogInformation("Stopped");
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken ct)
        {
            _logger.LogInformation("Processing message {messageId}", message.MessageId);

            if (TryGetSimpleMessage(message, out var messageContent))
            {
                _logger.LogInformation($"Received message with id '{message.MessageId}'. The content is '{messageContent.Title}'. The message will be removed from queue");

                // Send the ack 
                await _busClient.CompleteAsync(message.SystemProperties.LockToken);
                return;
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

        private bool TryGetSimpleMessage(Message incomingMessage, out AnsaNews outcomingMessage)
        {
            outcomingMessage = null;
            try
            {
                if (incomingMessage.Body != null && incomingMessage.Body.Length > 0)
                {
                    using (MemoryStream payloadStream = new MemoryStream(incomingMessage.Body, false))
                    using (StreamReader streamReader = new StreamReader(payloadStream, Encoding.UTF8))
                    using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                    {
                        // Please change the SimpleMessage objct with your own message type 
                        outcomingMessage = _serializer.Deserialize<AnsaNews>(jsonReader);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot parse payload from message {messageId}", incomingMessage.MessageId);
            }
            return false;
        }

        private bool TryGetStringMessage(Message incomingMessage, out string outcomingMessage)
        {
            outcomingMessage = null;
            try
            {
                if (incomingMessage.Body != null && incomingMessage.Body.Length > 0)
                {
                    using (MemoryStream payloadStream = new MemoryStream(incomingMessage.Body, false))
                    using (StreamReader streamReader = new StreamReader(payloadStream, Encoding.UTF8))
                    {
                        // Read the data as string
                        outcomingMessage = streamReader.ReadToEnd();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot parse payload from message {messageId}", incomingMessage.MessageId);
            }
            return false;
        }
    }
}
