using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;

public sealed class TransferOutput : IOutputType
{
    public Transaction Transaction { get; }
    public decimal UpdatedBalance { get; }

    public TransferOutput(IDebit debit, Money updatedBalance, Guid originAccountId, Guid destinationAccountId)
    {
        Debit debitEntity = (Debit)debit;

        Transaction = new Transaction(
            originAccountId,
            destinationAccountId,
            Debit.Description,
            debitEntity.Amount
            .ToMoney()
            .ToDecimal(),
            debitEntity.TransactionDate);

        UpdatedBalance = updatedBalance.ToDecimal();
    }
}