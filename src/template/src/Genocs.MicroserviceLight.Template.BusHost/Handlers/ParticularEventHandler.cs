using NServiceBus;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    public class ParticularEventHandler : IHandleMessages<Shared.Events.NServiceEventOccurred>
    {
        public Task Handle(Shared.Events.NServiceEventOccurred message, IMessageHandlerContext context)
        {
            // Do something with the message here
            return Task.CompletedTask;
        }
    }

}
