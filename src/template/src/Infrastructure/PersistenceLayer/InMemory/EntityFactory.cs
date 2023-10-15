using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.Customers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;


public sealed class EntityFactory : IEntityFactory
{
    public IAccount NewAccount(ICustomer customer)
    {
        var account = new Account(customer);
        return account;
    }

    public ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
    {
        var credit = new Credit(account, amountToDeposit, transactionDate);
        return credit;
    }

    public ICustomer NewCustomer(SSN ssn, Name name)
    {
        var customer = new Customer(ssn, name);
        return customer;
    }

    public IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
    {
        var debit = new Debit(account, amountToWithdraw, transactionDate);
        return debit;
    }
}