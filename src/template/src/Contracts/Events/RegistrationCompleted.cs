using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class RegistrationCompleted : IEvent
{
    public Guid CustomerId { get; init; }
    public Guid AccountId { get; init; }
    public Guid CreditId { get; init; }
}