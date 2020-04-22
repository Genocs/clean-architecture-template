namespace Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess
{
    using System;
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;

    public class Credit : Domain.Accounts.Credit
    {
        public Guid AccountId { get; protected set; }

        protected Credit() { }

        public Credit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
        {
            this.AccountId = account.Id;
            this.Amount = amountToDeposit;
            this.TransactionDate = transactionDate;
        }
    }
}