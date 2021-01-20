using System;

namespace Genocs.MicroserviceLight.Template.Shared.Events
{
    public class CloseAccountCompleted : Interfaces.IEvent
    {
        public Guid AccountId { get; set; }
    }
}
