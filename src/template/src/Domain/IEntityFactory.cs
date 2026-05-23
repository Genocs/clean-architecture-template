using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.Customers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Domain;

/// <summary>
/// The <c>IEntityFactory</c> interface defines a contract for creating instances of domain entities such as customers, accounts, credits, and debits.
/// It provides methods for instantiating these entities with the necessary parameters,
/// ensuring that the creation logic is centralized and consistent across the application.
/// </summary>
public interface IEntityFactory
{
    ICustomer NewCustomer(SSN ssn, Name name);
    IAccount NewAccount(ICustomer customer);
    ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate);
    IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate);
}