using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class DemoEventOccurred : IIntegrationEvent
{
    public string? Payload { get; init; }
    public int Value { get; init; }
}
