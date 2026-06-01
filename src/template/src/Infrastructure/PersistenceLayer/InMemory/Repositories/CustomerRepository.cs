using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;

public sealed class CustomerRepository(GenocsContext context) : ICustomerRepository
{
    private readonly GenocsContext _context = context;

    public async Task AddAsync(ICustomer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Add((Customer)customer);
        await Task.CompletedTask;
    }

    public async Task<ICustomer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = _context.Customers
            .SingleOrDefault(e => e.Id == id);

        if (customer == null)
        {
            return await Task.FromResult<Customer?>(null);
        }

        var accountIds = _context.Accounts
            .Where(e => e.CustomerId == id)
            .Select(e => e.Id)
            .ToList();

        customer.LoadAccounts(accountIds);

        return await Task.FromResult<Customer?>(customer);
    }

    public async Task UpdateAsync(ICustomer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Update((Customer)customer);
        await Task.CompletedTask;
    }
}