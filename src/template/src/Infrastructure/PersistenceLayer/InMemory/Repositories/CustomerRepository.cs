using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;


public sealed class CustomerRepository : ICustomerRepository
{
    private readonly GenocsContext _context;

    public CustomerRepository(GenocsContext context)
    {
        _context = context;
    }

    public async Task Add(ICustomer customer)
    {
        _context.Customers.Add((Customer)customer);
        await Task.CompletedTask;
    }

    public async Task<ICustomer> Get(Guid id)
    {
        var customer = _context.Customers
            .Where(e => e.Id == id)
            .SingleOrDefault();

        return await Task.FromResult<Customer>(customer);
    }

    public async Task Update(ICustomer customer)
    {
        var customerOld = _context.Customers
            .Where(e => e.Id == customer.Id)
            .SingleOrDefault();

        customerOld = (Customer)customer;
        await Task.CompletedTask;
    }
}