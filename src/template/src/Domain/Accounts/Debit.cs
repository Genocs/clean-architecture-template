namespace Genocs.MicroserviceLight.Template.Domain.Accounts
{
    using System;
    using ValueObjects;

    public class Debit : IDebit
    {
        public Guid Id { get; protected set; }
        public PositiveMoney Amount { get; protected set; }

        public string Description
        {
            get { return "Debit"; }
        }

        public DateTime TransactionDate { get; protected set; }

        public PositiveMoney Sum(PositiveMoney amount)
        {
            return Amount.Add(amount);
        }
    }
}