namespace Genocs.Domain
{
    using Genocs.Domain.Accounts;
    using Genocs.Domain.Customers;
    using Genocs.Domain.ValueObjects;
    using System;

    public interface IEntityFactory
    {
        ICustomer NewCustomer(SSN ssn, Name name);
        IAccount NewAccount(ICustomer customer);
        ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate);
        IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate);
    }
}