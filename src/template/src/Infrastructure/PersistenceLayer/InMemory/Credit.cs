namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Domain.Accounts;
    using Domain.ValueObjects;
    using System;

    public class Credit : Domain.Accounts.Credit
    {
        public Guid AccountId { get; protected set; }

        protected Credit() { }

        public Credit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
        {
            AccountId = account.Id;
            this.Amount = amountToDeposit;
            this.TransactionDate = transactionDate;
        }
    }
}