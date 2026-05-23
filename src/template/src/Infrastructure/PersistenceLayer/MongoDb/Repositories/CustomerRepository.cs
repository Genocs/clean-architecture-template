using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Customers;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Customer> _dbSetCustomer;

    public CustomerRepository(IMongoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _dbSetCustomer = _context.GetCollection<Customer>("Customers");
    }

    public Task AddAsync(ICustomer customer, CancellationToken cancellationToken = default)
    {
        _context.AddCommand(async () => await _dbSetCustomer.InsertOneAsync((Customer)customer, cancellationToken: cancellationToken));
        return Task.CompletedTask;
    }

    public async Task<ICustomer?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customers = await _dbSetCustomer.FindAsync(f => f.Id == id, cancellationToken: cancellationToken);
        if (customers != null)
        {
            return customers.FirstOrDefault();
        }

        return null;
    }

    public Task UpdateAsync(ICustomer customer, CancellationToken cancellationToken = default)
    {
        _context.AddCommand(() => _dbSetCustomer.ReplaceOneAsync(
            f => f.Id == customer.Id,
            (Customer)customer,
            cancellationToken: cancellationToken));

        return Task.CompletedTask;
    }
}
