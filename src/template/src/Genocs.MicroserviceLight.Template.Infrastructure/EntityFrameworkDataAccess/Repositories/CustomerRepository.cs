namespace Genocs.MicroserviceLight.Template.Infrastructure.EntityFrameworkDataAccess
{
    using Genocs.MicroserviceLight.Template.Application.Repositories;
    using Genocs.MicroserviceLight.Template.Domain.Customers;
    using Microsoft.EntityFrameworkCore;
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
            await _context.Customers.AddAsync((Customer)customer);
            await _context.SaveChangesAsync();
        }

        public async Task<ICustomer> Get(Guid id)
        {
            Customer customer = await _context.Customers
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            if (customer == null)
                return null;

            var accounts = _context.Accounts
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
}