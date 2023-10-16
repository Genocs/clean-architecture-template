using Genocs.CleanArchitecture.Template.Worker.Particular.Messages;
using Genocs.CleanArchitecture.Template.Worker.Particular.Services;
using NServiceBus;
using System.Threading.Tasks;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Handler
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
