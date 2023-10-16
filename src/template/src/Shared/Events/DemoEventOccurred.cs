using Genocs.CleanArchitecture.Template.Shared.Interfaces;

namespace Genocs.CleanArchitecture.Template.Shared.Events;

public class DemoEventOccurred : IIntegrationEvent
{
    public string? Payload { get; set; }
    public int Value { get; set; }
}
