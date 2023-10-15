using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;

/// <summary>
/// This event happen whenever the system receive a Load.
/// </summary>
public class TransactionLoaded : IEvent
{
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
}
