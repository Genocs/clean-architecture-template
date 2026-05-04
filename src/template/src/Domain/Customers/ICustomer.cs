using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.Common.Domain.Entities;

namespace Genocs.CleanArchitecture.Template.Domain.Customers;

public interface ICustomer : IAggregateRoot<Guid>
{
    AccountCollection Accounts { get; }
    void Register(IAccount account);
}