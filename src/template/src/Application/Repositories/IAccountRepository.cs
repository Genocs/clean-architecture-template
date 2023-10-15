using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Application.Repositories;

public interface IAccountRepository
{
    Task<IAccount> Get(Guid id);
    Task Add(IAccount account, ICredit credit);
    Task Update(IAccount account, ICredit credit);
    Task Update(IAccount account, IDebit debit);
    Task Delete(IAccount account);
}