using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;


public sealed class WithdrawOutput
{
    public Transaction Transaction { get; }
    public decimal UpdatedBalance { get; }

    public WithdrawOutput(IDebit debit, Money updatedBalance)
    {
        Debit debitEntity = (Debit)debit;

        Transaction = new Transaction(
            debitEntity.Description,
            debitEntity.Amount
            .ToMoney()
            .ToDecimal(),
            debitEntity.TransactionDate);

        UpdatedBalance = updatedBalance.ToDecimal();
    }
}