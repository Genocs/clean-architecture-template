namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer
{
    using Domain.Accounts;
    using Domain.ValueObjects;
    using System;

    public sealed class TransferOutput
    {
        public Transaction Transaction { get; }
        public decimal UpdatedBalance { get; }

        public TransferOutput(IDebit debit, Money updatedBalance, Guid originAccountId, Guid destinationAccountId)
        {
            Debit debitEntity = (Debit)debit;

            Transaction = new Transaction(
                originAccountId,
                destinationAccountId,
                debitEntity.Description,
                debitEntity.Amount
                .ToMoney()
                .ToDecimal(),
                debitEntity.TransactionDate);

            UpdatedBalance = updatedBalance.ToDecimal();
        }
    }
}