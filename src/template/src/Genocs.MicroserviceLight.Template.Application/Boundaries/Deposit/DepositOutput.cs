namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Deposit
{
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;

    public sealed class DepositOutput
    {
        public Transaction Transaction { get; }
        public decimal UpdatedBalance { get; }

        public DepositOutput(
            ICredit credit,
            Money updatedBalance)
        {
            Credit creditEntity = (Credit)credit;

            Transaction = new Transaction(
                creditEntity.Description,
                creditEntity
                .Amount
                .ToMoney()
                .ToDecimal(),
                creditEntity.TransactionDate);

            UpdatedBalance = updatedBalance.ToDecimal();
        }
    }
}