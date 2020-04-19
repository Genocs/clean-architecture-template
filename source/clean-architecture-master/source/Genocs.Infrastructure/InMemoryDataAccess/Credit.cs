namespace Genocs.Infrastructure.InMemoryDataAccess
{
    using System;
    using Genocs.Domain.Accounts;
    using Genocs.Domain.ValueObjects;

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