namespace Genocs.MicroserviceLight.Template.Application.Repositories
{
    using Domain.Accounts;
    using System;
    using System.Threading.Tasks;

    public interface IAccountRepository
    {
        Task<IAccount> Get(Guid id);
        Task Add(IAccount account, ICredit credit);
        Task Update(IAccount account, ICredit credit);
        Task Update(IAccount account, IDebit debit);
        Task Delete(IAccount account);
    }
}