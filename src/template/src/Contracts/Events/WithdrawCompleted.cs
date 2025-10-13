using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class WithdrawCompleted : IEvent
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
}
