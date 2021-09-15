namespace Genocs.MicroserviceLight.Template.BusWorkerMassTransit.Handlers
{
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using Shared.Events;
    using System.Threading.Tasks;

    public class DemoEventOccurredHandler : IConsumer<DemoEventOccurred>
    {
        private readonly ILogger<DemoEventOccurredHandler> _logger;

        public DemoEventOccurredHandler(ILogger<DemoEventOccurredHandler> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<DemoEventOccurred> context)
        {
            _logger.LogInformation(context.Message.Payload);
            return Task.CompletedTask;
        }
    }
}
