namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    using Shared.ReadModels;
    using System.Threading.Tasks;
    using Infrastructure.ServiceBus;
    using Microsoft.Extensions.Logging;

    public class AzureEventOccurredHandler : IMessageEventHandler<IntegrationEventIssued>
    {
        private readonly ILogger<AzureEventOccurredHandler> _logger;

        public AzureEventOccurredHandler(ILogger<AzureEventOccurredHandler> logger)
            => _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        public Task Handle(IntegrationEventIssued @event)
        {
            _logger.LogDebug("Processed message");
            // Do something with the message here
            return Task.CompletedTask;
        }
    }
}
