using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Contracts.Events;

public class CloseAccountCompleted : IEvent
{
    public Guid AccountId { get; set; }
}
