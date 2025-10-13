using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class IntegrationEventIssued : IIntegrationEvent
{
    public string? Title { get; set; }
}
