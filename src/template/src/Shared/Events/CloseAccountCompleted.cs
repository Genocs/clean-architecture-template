namespace Genocs.CleanArchitecture.Template.Shared.Events;

public class CloseAccountCompleted : Interfaces.IEvent
{
    public Guid AccountId { get; set; }
}
