using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

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