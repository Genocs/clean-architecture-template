using NServiceBus;

namespace Genocs.CleanArchitecture.Template.WorkerNServiceBus.Messages;

public class DemoMessage : ICommand
{
    public string Payload { get; set; } = default!;

    public int Value { get; set; }
}
