using NServiceBus;

namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.Commands;

/// <summary>
/// This is a command message that is triggered by time.
/// It's used to demonstrate sending commands via NServiceBus.
/// </summary>
public class TimeTriggreredCommand : ICommand
{
    public DateTime TriggeredAt { get; init; } = DateTime.UtcNow;
    public required string Payload { get; init; }
}
