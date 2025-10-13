using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class DepositCompleted : IEvent
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
}
