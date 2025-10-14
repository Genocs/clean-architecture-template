namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Rebus;

public class RebusBusSettings
{
    public const string Position = "RebusBusSettings";
    public bool Enabled { get; set; }

    public string? TransportConnection { get; set; }

    public string? QueueName { get; set; }
}