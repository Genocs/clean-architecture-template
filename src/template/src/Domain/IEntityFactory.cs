namespace Genocs.MicroserviceLight.Template.Domain
{
    using Accounts;
    using Customers;
    using System;
    using ValueObjects;

    public interface IEntityFactory
    {
        ICustomer NewCustomer(SSN ssn, Name name);
        IAccount NewAccount(ICustomer customer);
        ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate);
        IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate);
    }
}