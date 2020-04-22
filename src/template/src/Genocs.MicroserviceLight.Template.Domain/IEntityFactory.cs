namespace Genocs.MicroserviceLight.Template.Domain
{
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using Genocs.MicroserviceLight.Template.Domain.Customers;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using System;

    public interface IEntityFactory
    {
        ICustomer NewCustomer(SSN ssn, Name name);
        IAccount NewAccount(ICustomer customer);
        ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate);
        IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate);
    }
}