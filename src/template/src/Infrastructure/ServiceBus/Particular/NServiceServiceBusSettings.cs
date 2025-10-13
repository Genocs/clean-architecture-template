namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Particular;

public class NServiceServiceBusSettings
{
    public string? EndpointName { get; set; }
    public string? TransportConnectionString { get; set; }

    public string? PersistenceConnectionString { get; set; }
    public string? PersistenceDatabase { get; set; }
}
