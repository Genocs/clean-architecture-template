namespace Genocs.MicroserviceLight.Template.Shared.Events
{
    public class NServiceEventOccurred : Interfaces.IEvent, NServiceBus.IEvent
    {
        public string EventId { get; set; }
    }
}
