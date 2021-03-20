namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories
{
    using Application.Repositories;
    using Domain.Customers;
    using MongoDB.Driver;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoContext _context;
        private readonly IMongoCollection<MongoDb.Customer> _DbSetCustomer;

        public CustomerRepository(IMongoContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _DbSetCustomer = _context.GetCollection<PersistenceLayer.MongoDb.Customer>("Customers");
        }

        public Task Add(ICustomer customer)
        {
            _context.AddCommand(async () => await _DbSetCustomer.InsertOneAsync((MongoDb.Customer)customer));
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
            => await _DbSetCustomer.FindOneAndReplaceAsync(f => f.Id == customer.Id, (MongoDb.Customer)customer);
    }
}
