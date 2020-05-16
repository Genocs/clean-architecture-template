namespace Genocs.MicroserviceLight.Template.Infrastructure.MongoDbDataAccess.Repositories
{
    using Application.Repositories;
    using Domain.Customers;
    using MongoDB.Driver;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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
            await _context.Customers.InsertOneAsync((MongoDbDataAccess.Customer)customer);
        }

        public async Task<ICustomer> Get(Guid id)
        {
            var customers = await _context.GetCollection<MongoDbDataAccess.Customer>("Customers").FindAsync(f => f.Id == id);
            MongoDbDataAccess.Customer customer = customers.FirstOrDefault();
            return customer;
        }

        public async Task Update(ICustomer customer)
            => await _context.Credits.FindOneAndReplaceAsync(f => f.Id == customer.Id, (MongoDbDataAccess.Credit)customer);
    }
}
