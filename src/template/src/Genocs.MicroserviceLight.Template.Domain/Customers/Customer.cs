namespace Genocs.MicroserviceLight.Template.Domain.Customers
{
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using System;

    public class Customer : ICustomer
    {
        public Guid Id { get; protected set; }
        public Name Name { get; protected set; }
        public SSN SSN { get; protected set; }
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
}