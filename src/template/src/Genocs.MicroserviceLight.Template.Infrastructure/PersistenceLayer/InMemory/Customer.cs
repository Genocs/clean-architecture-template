namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Domain.Customers;
    using Domain.ValueObjects;
    using System;
    using System.Collections.Generic;

    public class Customer : Domain.Customers.Customer
    {
        public Customer() { }

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
}