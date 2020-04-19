namespace Genocs.Infrastructure.EntityFrameworkDataAccess
{
    using Genocs.Domain.Customers;
    using Genocs.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;

    public class Customer : Domain.Customers.Customer
    {
        protected Customer() { }

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