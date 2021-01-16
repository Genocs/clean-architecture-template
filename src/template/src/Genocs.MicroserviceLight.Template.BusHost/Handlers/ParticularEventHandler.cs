using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    public class ParticularEventHandler : IHandleMessages<Shared.Events.EventOccurred>
    {
        static ILog _logger = LogManager.GetLogger<ParticularEventHandler>();

        public Task Handle(Shared.Events.EventOccurred message, IMessageHandlerContext context)
        {

            _logger.Info($"NServiceEventOccurred. Received message with EventId: '{message.EventId}'");

            // Do something with the message here
            return Task.CompletedTask;
        }
    }

}
