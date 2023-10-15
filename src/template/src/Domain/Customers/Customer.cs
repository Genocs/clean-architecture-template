using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Domain.Customers;

public class Customer : ICustomer
{
    public Guid Id { get; protected set; }
    public Name? Name { get; protected set; }
    public SSN? SSN { get; protected set; }
    public AccountCollection Accounts { get; protected set; }

    public Customer()
    {
        Accounts = new AccountCollection();
    }

    public void Register(IAccount account)
    {
        if (Accounts == null)
            Accounts = new AccountCollection();

        Accounts.Add(account.Id);
    }
}