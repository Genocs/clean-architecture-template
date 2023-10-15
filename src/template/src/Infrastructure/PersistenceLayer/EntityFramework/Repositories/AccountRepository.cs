using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework.Repositories;


public sealed class AccountRepository : IAccountRepository
{
    private readonly GenocsContext _context;

    public AccountRepository(GenocsContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(IAccount account, ICredit credit)
    {
        await _context.Accounts.AddAsync((Account)account);
        await _context.Credits.AddAsync((Credit)credit);
    }

    public async Task Delete(IAccount account)
    {
        string deleteSQL =
            @"DELETE FROM Credit WHERE AccountId = @Id;
                      DELETE FROM Debit WHERE AccountId = @Id;
                      DELETE FROM Account WHERE Id = @Id;";

        SqlParameter id = new("@Id", account.Id);
        _ = await _context.Database.ExecuteSqlRawAsync(deleteSQL, id);
    }

    public async Task<IAccount> Get(Guid id)
    {
        var account = await _context
            .Accounts
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync();

        if (account == null)
        {
            return null;
        }

        List<Credit> credits = _context.Credits
            .Where(e => e.AccountId == id)
            .ToList();

        List<Debit> debits = _context.Debits
            .Where(e => e.AccountId == id)
            .ToList();

        account.Load(credits, debits);

        return account;
    }

    public async Task Update(IAccount account, ICredit credit)
        => await _context.Credits.AddAsync((Credit)credit);

    public async Task Update(IAccount account, IDebit debit)
        => await _context.Debits.AddAsync((Debit)debit);
}