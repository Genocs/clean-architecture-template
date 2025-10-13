using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class DemoEventOccurred : IIntegrationEvent
{
    public string? Payload { get; set; }
    public int Value { get; set; }
}
