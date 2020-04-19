namespace Genocs.Infrastructure.InMemoryDataAccess
{
    using System;
    using Genocs.Domain.Accounts;
    using Genocs.Domain.ValueObjects;

    public class Debit : Domain.Accounts.Debit
    {
        public Guid AccountId { get; protected set; }

        protected Debit() { }

        public Debit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
        {
            this.AccountId = account.Id;
            this.Amount = amountToWithdraw;
            this.TransactionDate = transactionDate;
        }
    }
}