using Genocs.CleanArchitecture.Template.Shared.Interfaces;

namespace Genocs.CleanArchitecture.Template.Shared.Events;

public class IntegrationEventIssued : IIntegrationEvent
{
    public string? Title { get; set; }
}
