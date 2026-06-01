using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class IntegrationEventIssued : IIntegrationEvent
{
    public string? Title { get; init; }
}
