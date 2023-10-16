using Genocs.CleanArchitecture.Template.Domain.Customers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework;

public class Customer : Domain.Customers.Customer
{
    protected Customer()
    {
    }

    public Customer(SSN ssn, Name name)
    {
        Id = Guid.NewGuid();
        SSN = ssn;
        Name = name;
    }

    public void LoadAccounts(IEnumerable<Guid> accounts)
    {
        Accounts = new AccountCollection();
        Accounts.Add(accounts);
    }
}