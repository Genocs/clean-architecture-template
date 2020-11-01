namespace Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess.Repositories
{
    using Application.Repositories;
    using Domain.Customers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly GenocsContext _context;

        public CustomerRepository(GenocsContext context)
        {
            _context = context;
        }

        public async Task Add(ICustomer customer)
        {
            _context.Customers.Add((InMemoryDataAccess.Customer)customer);
            await Task.CompletedTask;
        }

        public async Task<ICustomer> Get(Guid id)
        {
            Customer customer = _context.Customers
                .Where(e => e.Id == id)
                .SingleOrDefault();

            return await Task.FromResult<Customer>(customer);
        }

        public async Task Update(ICustomer customer)
        {
            Customer customerOld = _context.Customers
                .Where(e => e.Id == customer.Id)
                .SingleOrDefault();

            customerOld = (Customer)customer;
            await Task.CompletedTask;
        }
    }
}