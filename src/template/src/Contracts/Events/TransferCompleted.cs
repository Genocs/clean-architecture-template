using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public sealed class TransferCompleted : IEvent
{
    public Guid OriginalAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}
