namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus
{
    public class ParticularServiceBusSettings
    {
        public string EndpointName { get; set; }
        public string TransportConnectionString { get; set; }

        public string PersistenceConnectionString { get; set; }
        public string PersistenceDatabase { get; set; }

    }
}
