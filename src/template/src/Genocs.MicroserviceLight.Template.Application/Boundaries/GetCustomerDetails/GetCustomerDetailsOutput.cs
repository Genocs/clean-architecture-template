namespace Genocs.MicroserviceLight.Template.Application.Boundaries.GetCustomerDetails
{
    using Domain.Customers;
    using System;
    using System.Collections.Generic;

    public sealed class GetCustomerDetailsOutput
    {
        public Guid CustomerId { get; }
        public string SSN { get; }
        public string Name { get; }
        public IReadOnlyList<Account> Accounts { get; }

        public GetCustomerDetailsOutput(
            ICustomer customer,
            List<Account> accounts)
        {
            Customer customerEntity = (Customer)customer;
            CustomerId = customerEntity.Id;
            SSN = customerEntity.SSN.ToString();
            Name = customerEntity.Name.ToString();
            Accounts = accounts;
        }
    }
}