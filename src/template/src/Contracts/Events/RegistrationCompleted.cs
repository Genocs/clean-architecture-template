using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public sealed class RegistrationCompleted : IEvent
{
    public Guid CustomerId { get; set; }
    public Guid AccountId { get; set; }
    public Guid CreditId { get; set; }
}
