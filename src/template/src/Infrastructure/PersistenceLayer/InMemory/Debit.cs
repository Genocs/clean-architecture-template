namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Domain.Accounts;
    using Domain.ValueObjects;
    using System;

    public class Debit : Domain.Accounts.Debit
    {
        public Guid AccountId { get; protected set; }

        protected Debit() { }

        public Debit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
        {
            AccountId = account.Id;
            this.Amount = amountToWithdraw;
            this.TransactionDate = transactionDate;
        }
    }
}