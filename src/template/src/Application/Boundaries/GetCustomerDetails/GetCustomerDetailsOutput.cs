using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

public sealed class GetCustomerDetailsOutput
{
    public Guid CustomerId { get; }
    public string SSN { get; }
    public string Name { get; }
    public IReadOnlyList<Account> Accounts { get; }

    public GetCustomerDetailsOutput(ICustomer customer, List<Account> accounts)
    {
        Customer customerEntity = (Customer)customer ?? throw new ArgumentNullException(nameof(customer));
        Accounts = accounts ?? throw new ArgumentNullException(nameof(accounts));
        CustomerId = customerEntity.Id;
        SSN = customerEntity.SSN.ToString();
        Name = customerEntity.Name.ToString();
    }
}