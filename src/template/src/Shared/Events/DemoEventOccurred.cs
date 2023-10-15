namespace Genocs.MicroserviceLight.Template.Shared.Events
{
    public class DemoEventOccurred : Interfaces.IIntegrationEvent
    {
        public string Payload { get; set; }
        public int Value { get; set; }
    }
}
