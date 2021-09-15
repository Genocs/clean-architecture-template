namespace Genocs.MicroserviceLight.Template.BusWorkerParticular.Messages
{
    using NServiceBus;

    public class DemoMessage : ICommand
    {
        public string Payload { get; set; }

        public int Value { get; set; }
    }

}
