using NServiceBus;

namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.TransactionSaga;

/// <summary>
/// This event happened when the system receive an Unload.
/// </summary>
public class RedemptionCompleted : IEvent
{
   /// <summary>
   /// The requestId.
   /// </summary>
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
}
