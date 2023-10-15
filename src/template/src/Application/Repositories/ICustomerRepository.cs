using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Application.Repositories;

public interface ICustomerRepository
{
    Task<ICustomer> Get(Guid id);
    Task Add(ICustomer customer);
    Task Update(ICustomer customer);
}