namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    using Infrastructure.ServiceBus.Azure;
    using Shared.ReadModels;
    using NServiceBus.Logging;
    using System.Threading.Tasks;

    public class AzureEventOccurredHandler : IMessageEventHandler<IntegrationEventIssued>
    {
        static ILog _logger = LogManager.GetLogger<AzureEventOccurredHandler>();

        public Task Handle(IntegrationEventIssued @event)
        {
            _logger.Debug("Processed message");
            // Do something with the message here
            return Task.CompletedTask;
        }
    }
}
