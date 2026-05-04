using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework.Repositories;

public sealed class AccountRepository(GenocsContext context) : IAccountRepository
{
    private readonly GenocsContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
    {
        await _context.Accounts.AddAsync((Account)account, cancellationToken);
        await _context.Credits.AddAsync((Credit)credit, cancellationToken);
    }

    public async Task DeleteAsync(IAccount account, CancellationToken cancellationToken = default)
    {
        string deleteSQL =
            @"DELETE FROM Credit WHERE AccountId = @Id;
                      DELETE FROM Debit WHERE AccountId = @Id;
                      DELETE FROM Account WHERE Id = @Id;";

        SqlParameter id = new("@Id", account.Id);
        _ = await _context.Database.ExecuteSqlRawAsync(deleteSQL, id);
    }

    public async Task<IAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _context
            .Accounts
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

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

    public async Task UpdateAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
        => await _context.Credits.AddAsync((Credit)credit, cancellationToken);

    public async Task UpdateAsync(IAccount account, IDebit debit, CancellationToken cancellationToken = default)
        => await _context.Debits.AddAsync((Debit)debit, cancellationToken);
}