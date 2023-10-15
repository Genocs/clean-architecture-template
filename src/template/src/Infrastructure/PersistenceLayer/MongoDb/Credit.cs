namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb
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
            Amount = amountToDeposit;
            TransactionDate = transactionDate;
        }
    }
}
