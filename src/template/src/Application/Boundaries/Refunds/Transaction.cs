namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public sealed class Transaction(string description, decimal amount, DateTime transactionDate)
{
    public string Description { get; } = description;
    public decimal Amount { get; } = amount;
    public DateTime TransactionDate { get; } = transactionDate;
}