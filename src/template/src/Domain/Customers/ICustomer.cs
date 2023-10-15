using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Domain.Customers;

public interface ICustomer : IAggregateRoot
{
    AccountCollection Accounts { get; }
    void Register(IAccount account);
}