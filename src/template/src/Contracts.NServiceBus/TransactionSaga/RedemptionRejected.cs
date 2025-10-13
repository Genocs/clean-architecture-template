using NServiceBus;

namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.TransactionSaga;

/// <summary>
/// This event happen when the system receive an Unload.
/// </summary>
public class RedemptionRejected : IEvent
{
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
}
