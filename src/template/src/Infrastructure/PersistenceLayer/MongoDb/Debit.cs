namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb
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
            Amount = amountToWithdraw;
            TransactionDate = transactionDate;
        }
    }
}
