using Genocs.MicroserviceLight.Template.Shared.Events;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    class Handler : IHandleMessages<EventOccurred>
    {
        public async Task Handle(EventOccurred message)
        {
            Console.WriteLine("Got string: {0}", message.EventId);
            await Task.CompletedTask;
        }
    }
}
