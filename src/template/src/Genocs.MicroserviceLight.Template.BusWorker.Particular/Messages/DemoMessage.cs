using NServiceBus;

namespace Genocs.MicroserviceLight.Template.ParticularBusWorker.Messages
{
    public class DemoMessage : ICommand
    {
        public string Payload { get; set; }

        public int Value { get; set; }
    }
}
