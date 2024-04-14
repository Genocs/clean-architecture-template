using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Messages;

public class DemoMessage : ICommand
{
    public string Payload { get; set; } = default!;

    public int Value { get; set; }
}
