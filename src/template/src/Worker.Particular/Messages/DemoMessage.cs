using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Messages
{
    public class DemoMessage : ICommand
    {
        public string Payload { get; set; }

        public int Value { get; set; }
    }
}
