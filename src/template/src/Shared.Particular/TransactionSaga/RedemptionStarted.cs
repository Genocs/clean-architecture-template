using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;

/// <summary>
/// This event happen when the system start processing the redemption.
/// </summary>
public class RedemptionStarted : IEvent
{
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
}
