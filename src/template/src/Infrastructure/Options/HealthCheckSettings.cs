namespace Genocs.CleanArchitecture.Template.Infrastructure.Options;

public class HealthCheckSettings
{
    public const string Position = "HealthChecks";

    public bool Enabled { get; set; } = true;
    public int EvaluationTimeInSeconds { get; set; } = 10;
    public int MaximumHistoryEntriesPerEndpoint { get; set; } = 60;
    public int MinimumSecondsBetweenFailureNotifications { get; set; } = 60;
    public int MemoryThresholdMB { get; set; } = 500;
    public int MemoryCriticalThresholdMB { get; set; } = 1024;
}
