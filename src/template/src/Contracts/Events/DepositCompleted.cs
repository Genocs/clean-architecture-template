using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public sealed class DepositCompleted : IEvent
{
    public Guid AccountId { get; init; }
    public decimal Amount { get; init; }
}
