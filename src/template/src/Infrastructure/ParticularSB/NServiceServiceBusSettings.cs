using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Infrastructure.ParticularSB;

public class NServiceServiceBusSettings
{
    public static string Position = "NServiceBus";

    public string? EndpointName { get; set; }
    public string? TransportConnectionString { get; set; }
    public bool UsePersistence { get; set; }
    public string? PersistenceConnectionString { get; set; }
    public string? PersistenceDatabase { get; set; }


}