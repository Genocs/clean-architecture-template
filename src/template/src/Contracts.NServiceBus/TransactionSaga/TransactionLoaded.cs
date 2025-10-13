using NServiceBus;

namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.TransactionSaga;

/// <summary>
/// This event happen whenever the system receive a Load.
/// </summary>
public class TransactionLoaded : IEvent
{
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
}
