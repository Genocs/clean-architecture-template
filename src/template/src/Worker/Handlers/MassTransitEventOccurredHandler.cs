namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    using Infrastructure.ServiceBus;
    using Microsoft.Extensions.Logging;
    using Shared.ReadModels;
    using System.Threading.Tasks;

    public class MassTransitEventOccurredHandler : IMessageEventHandler<IntegrationEventIssued>
    {
        private readonly ILogger<MassTransitEventOccurredHandler> _logger;

        public MassTransitEventOccurredHandler(ILogger<MassTransitEventOccurredHandler> logger)
            => _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        public Task Handle(IntegrationEventIssued @event)
        {
            _logger.LogDebug("Processed message");
            // Do something with the message here
            return Task.CompletedTask;
        }
    }
}
