namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;

public sealed class Transaction(Guid originAccountId, Guid destinationAccountId, string description, decimal amount, DateTime transactionDate)
{
    public Guid OriginAccountId { get; } = originAccountId;
    public Guid DestinationAccountId { get; } = destinationAccountId;
    public string Description { get; } = description;
    public decimal Amount { get; } = amount;
    public DateTime TransactionDate { get; } = transactionDate;
}