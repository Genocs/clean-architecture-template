namespace Genocs.MicroserviceLight.Template.Application.Repositories
{
    using Domain.Customers;
    using System;
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {
        Task<ICustomer> Get(Guid id);
        Task Add(ICustomer customer);
        Task Update(ICustomer customer);
    }
}