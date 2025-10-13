namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class TransferCompleted : Interfaces.IEvent
{
    public Guid OriginalAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}
