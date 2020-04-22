namespace Genocs.MicroserviceLight.Template.Infrastructure.EntityFrameworkDataAccess
{
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using System;

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