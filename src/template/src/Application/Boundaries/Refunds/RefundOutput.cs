using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public sealed class RefundOutput
{
    public Transaction Transaction { get; }
    public decimal UpdatedBalance { get; }

    public RefundOutput(IDebit debit, Money updatedBalance)
    {
        Debit debitEntity = (Debit)debit;

        Transaction = new Transaction(
            Debit.Description,
            debitEntity.Amount
            .ToMoney()
            .ToDecimal(),
            debitEntity.TransactionDate);

        UpdatedBalance = updatedBalance.ToDecimal();
    }
}