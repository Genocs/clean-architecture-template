using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;

/// <summary>
/// This is the SagaData Object.
/// </summary>
public class TransactionSagaData : ContainSagaData
{
    public string? RequestId { get; set; }
    public string? TransactionId { get; set; }
    public string? TransactionStatus { get; set; }

    public string? Property1 { get; set; }
    public string? Property2 { get; set; }
}
