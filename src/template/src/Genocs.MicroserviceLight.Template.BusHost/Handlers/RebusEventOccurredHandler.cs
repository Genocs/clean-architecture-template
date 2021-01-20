using Genocs.MicroserviceLight.Template.Shared.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    class RebusEventOccurredHandler : IHandleMessages<EventOccurred>
    {

        private readonly ILogger<RebusEventOccurredHandler> _logger;

        public RebusEventOccurredHandler()
        {

        }

        public RebusEventOccurredHandler(ILogger<RebusEventOccurredHandler> logger)
        {
            this._logger = logger;
        }

        public async Task Handle(EventOccurred message)
        {
            // Add Logger
            //_logger.LogInformation("Got string: {0}", message.EventId);
            await Task.CompletedTask;
        }
    }
}
