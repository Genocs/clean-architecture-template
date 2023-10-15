using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Customers;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories
{


    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoContext _context;
        private readonly IMongoCollection<Customer> _DbSetCustomer;

        public CustomerRepository(IMongoContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _DbSetCustomer = _context.GetCollection<Customer>("Customers");
        }

        public Task Add(ICustomer customer)
        {
            _context.AddCommand(async () => await _DbSetCustomer.InsertOneAsync((Customer)customer));
            return Task.CompletedTask;
        }

        public async Task<ICustomer> Get(Guid id)
        {
            var customers = await _DbSetCustomer.FindAsync(f => f.Id == id);
            if (customers != null)
            {
                return customers.FirstOrDefault();
            }

            return null;
        }

        public async Task Update(ICustomer customer)
            => await _DbSetCustomer.FindOneAndReplaceAsync(f => f.Id == customer.Id, (Customer)customer);
    }
}
