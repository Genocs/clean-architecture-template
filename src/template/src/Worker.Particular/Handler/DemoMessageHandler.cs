using Genocs.MicroserviceLight.Template.ParticularBusWorker.Messages;
using Genocs.MicroserviceLight.Template.ParticularBusWorker.Services;
using NServiceBus;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.ParticularBusWorker.Handler
{
    public class DemoMessageHandler : IHandleMessages<DemoMessage>
    {
        private readonly ICalculateStuff stuffCalculator;

        public DemoMessageHandler(ICalculateStuff stuffCalculator)
        {
            this.stuffCalculator = stuffCalculator;
        }

        public async Task Handle(DemoMessage message, IMessageHandlerContext context)
        {
            await stuffCalculator.Calculate(message.Value);

            // Do some more stuff if needed
        }
    }
}
