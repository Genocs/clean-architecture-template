namespace Genocs.MicroserviceLight.Template.BusWorkerParticular.Handler
{
    using BusWorkerParticular.Messages;
    using BusWorkerParticular.Services;
    using NServiceBus;
    using System.Threading.Tasks;

    public class DemoMessageHandler : IHandleMessages<DemoMessage>
    {
        //private readonly ICalculateStuff _stuffCalculator;

        public DemoMessageHandler(/*ICalculateStuff stuffCalculator*/)
        {
            //_stuffCalculator = stuffCalculator;
        }

        public async Task Handle(DemoMessage message, IMessageHandlerContext context)
        {
           // await _stuffCalculator.Calculate(message.Value);
            await Task.CompletedTask;

            // Do some more stuff if needed
        }
    }

}
