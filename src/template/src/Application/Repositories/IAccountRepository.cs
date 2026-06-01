using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Application.Repositories;

public interface IAccountRepository
{
    Task<IAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default);
    Task UpdateAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default);
    Task UpdateAsync(IAccount account, IDebit debit, CancellationToken cancellationToken = default);
    Task DeleteAsync(IAccount account, CancellationToken cancellationToken = default);
}