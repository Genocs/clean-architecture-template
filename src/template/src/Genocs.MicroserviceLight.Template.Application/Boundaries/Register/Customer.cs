namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Register
{
    using Genocs.MicroserviceLight.Template.Domain.Customers;
    using System;
    using System.Collections.Generic;

    public sealed class Customer
    {
        public Guid CustomerId { get; }
        public string SSN { get; }
        public string Name { get; }
        public IReadOnlyList<Account> Accounts { get; }

        public Customer(
            ICustomer customer,
            List<Account> accounts)
        {
            var customerEntity = (Domain.Customers.Customer)customer;
            CustomerId = customerEntity.Id;
            SSN = customerEntity.SSN.ToString();
            Name = customerEntity.Name.ToString();
            Accounts = accounts;
        }
    }
}