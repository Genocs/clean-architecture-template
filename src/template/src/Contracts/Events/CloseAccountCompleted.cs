using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public sealed class CloseAccountCompleted : IEvent
{
    public Guid AccountId { get; init; }
}
