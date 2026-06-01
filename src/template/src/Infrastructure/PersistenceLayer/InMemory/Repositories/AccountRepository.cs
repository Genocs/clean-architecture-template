using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;

public sealed class AccountRepository(GenocsContext context) : IAccountRepository
{
    private readonly GenocsContext _context = context;

    public async Task AddAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Add((Account)account);
        _context.Credits.Add((Credit)credit);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(IAccount account, CancellationToken cancellationToken = default)
    {
        var accountOld = _context.Accounts
            .Single(e => e.Id == account.Id);

        _context.Accounts.Remove(accountOld);

        await Task.CompletedTask;
    }

    public async Task<IAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = _context.Accounts
            .SingleOrDefault(e => e.Id == id);

        if (account == null)
        {
            return await Task.FromResult<Account?>(null);
        }

        var credits = _context.Credits
            .Where(e => e.AccountId == id)
            .ToList();

        var debits = _context.Debits
            .Where(e => e.AccountId == id)
            .ToList();

        account.Load(credits, debits);

        return await Task.FromResult<Account?>(account);
    }

    public async Task UpdateAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Update((Account)account);
        _context.Credits.Add((Credit)credit);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(IAccount account, IDebit debit, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Update((Account)account);
        _context.Debits.Add((Debit)debit);
        await Task.CompletedTask;
    }
}