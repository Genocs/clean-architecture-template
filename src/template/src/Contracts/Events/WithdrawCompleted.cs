using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public sealed class WithdrawCompleted : IEvent
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
}
