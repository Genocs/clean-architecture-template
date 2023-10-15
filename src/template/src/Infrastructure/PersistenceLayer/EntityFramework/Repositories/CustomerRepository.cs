using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework.Repositories;


public sealed class CustomerRepository : ICustomerRepository
{
    private readonly GenocsContext _context;

    public CustomerRepository(GenocsContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(ICustomer customer)
    {
        await _context.Customers.AddAsync((Customer)customer);
        await _context.SaveChangesAsync();
    }

    public async Task<ICustomer> Get(Guid id)
    {
        var customer = await _context.Customers
            .Where(c => c.Id == id)
            .SingleOrDefaultAsync();

        if (customer == null)
        {
            return null;
        }

        List<Guid> accounts = _context.Accounts
            .Where(e => e.CustomerId == id)
            .Select(e => e.Id)
            .ToList();

        customer.LoadAccounts(accounts);

        return customer;
    }

    public async Task Update(ICustomer customer)
    {
        _context.Customers.Update((Customer)customer);
        await _context.SaveChangesAsync();
    }
}