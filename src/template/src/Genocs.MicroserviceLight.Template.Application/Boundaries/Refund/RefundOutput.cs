namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Refund
{
    using Domain.Accounts;
    using Domain.ValueObjects;

    public sealed class RefundOutput
    {
        public Transaction Transaction { get; }
        public decimal UpdatedBalance { get; }

        public RefundOutput(IDebit debit, Money updatedBalance)
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
}