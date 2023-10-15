namespace Genocs.CleanArchitecture.Template.Shared.Events;

public class TransferCompleted : Interfaces.IEvent
{
    public Guid OriginalAccountId { get; set; }
    public Guid DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}
