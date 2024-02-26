using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;

public class Account : Domain.Accounts.Account
{
    public Guid CustomerId { get; protected set; }

    protected Account()
    {
    }

    public Account(ICustomer customer)
    {
        Id = Guid.NewGuid();
        CustomerId = customer.Id;
    }

    public void Load(IList<Credit> credits, IList<Debit> debits)
    {
        Credits = new CreditsCollection();
        Credits.Add(credits);

        Debits = new DebitsCollection();
        Debits.Add(debits);
    }
}