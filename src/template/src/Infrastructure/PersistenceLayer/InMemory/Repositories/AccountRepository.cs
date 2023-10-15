using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;


public sealed class AccountRepository : IAccountRepository
{
    private readonly GenocsContext _context;

    public AccountRepository(GenocsContext context)
    {
        _context = context;
    }

    public async Task Add(IAccount account, ICredit credit)
    {
        _context.Accounts.Add((Account)account);
        _context.Credits.Add((Credit)credit);
        await Task.CompletedTask;
    }

    public async Task Delete(IAccount account)
    {
        var accountOld = _context.Accounts
            .Where(e => e.Id == account.Id)
            .SingleOrDefault();

        _context.Accounts.Remove(accountOld);

        await Task.CompletedTask;
    }

    public async Task<IAccount> Get(Guid id)
    {
        var account = _context.Accounts
            .Where(e => e.Id == id)
            .SingleOrDefault();

        return await Task.FromResult<Account>(account);
    }

    public async Task Update(IAccount account, ICredit credit)
    {
        var accountOld = _context.Accounts
            .Where(e => e.Id == account.Id)
            .SingleOrDefault();

        accountOld = (Account)account;
        await Task.CompletedTask;
    }

    public async Task Update(IAccount account, IDebit debit)
    {
        var accountOld = _context.Accounts
            .Where(e => e.Id == account.Id)
            .SingleOrDefault();

        accountOld = (Account)account;
        await Task.CompletedTask;
    }
}