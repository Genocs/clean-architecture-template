using Genocs.MicroserviceLight.Template.Shared.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    class RebusEventOccurredHandler : IHandleMessages<RegistrationCompleted>
    {

        private readonly ILogger<RebusEventOccurredHandler> _logger;

        public RebusEventOccurredHandler()
        {

        }

        public RebusEventOccurredHandler(ILogger<RebusEventOccurredHandler> logger) => _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        public async Task Handle(RegistrationCompleted message)
        {
            _logger.LogInformation("Got string: {0}", message.CreditId);
            await Task.CompletedTask;
        }
    }
}
