namespace Genocs.CleanArchitecture.Template.Infrastructure.AzureSB;

public class AzureServiceBusSettings
{
    public const string Position = "AzureServiceBusSettings";

    public bool Enabled { get; set; }

    public string? QueueEndpoint { get; set; }

    public string? QueueName { get; set; }

    public string? QueueAccessPolicyName { get; set; }

    public string? QueueAccessPolicyKey { get; set; }

    public int MaxConcurrency { get; internal set; } = 20;

    public int PrefetchCount { get; internal set; } = 3000;
}
