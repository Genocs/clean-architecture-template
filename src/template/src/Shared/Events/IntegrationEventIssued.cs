namespace Genocs.MicroserviceLight.Template.Shared.ReadModels
{
    using Interfaces;

    public class IntegrationEventIssued : IIntegrationEvent
    {
        public string Title { get; set; }
    }
}
