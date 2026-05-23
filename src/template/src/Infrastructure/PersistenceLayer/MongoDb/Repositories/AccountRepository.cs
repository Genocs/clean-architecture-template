using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Account> _dbSetAccount;
    private readonly IMongoCollection<Credit> _dbSetCredit;
    private readonly IMongoCollection<Debit> _dbSetDebit;

    public AccountRepository(IMongoContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));

        _dbSetAccount = _context.GetCollection<Account>("Accounts");
        _dbSetCredit = _context.GetCollection<Credit>("Credits");
        _dbSetDebit = _context.GetCollection<Debit>("Debits");
    }

    public Task AddAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
    {
        _context.AddCommand(() => _dbSetAccount.InsertOneAsync((Account)account, cancellationToken: cancellationToken));
        _context.AddCommand(() => _dbSetCredit.InsertOneAsync((Credit)credit, cancellationToken: cancellationToken));
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(IAccount account, CancellationToken cancellationToken = default)
        => _ = await _dbSetAccount.DeleteOneAsync(d => d.Id == account.Id, cancellationToken);

    public async Task<IAccount?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var accounts = await _dbSetAccount.FindAsync(f => f.Id == id, cancellationToken: cancellationToken);
        var account = accounts.FirstOrDefault();

        if (account == null)
        {
            return null;
        }

        var credits = await _dbSetCredit.FindAsync(f => f.AccountId == account.Id, cancellationToken: cancellationToken);
        var debits = await _dbSetDebit.FindAsync(f => f.AccountId == account.Id, cancellationToken: cancellationToken);

        account.Load(credits.ToList(), debits.ToList());

        return account;
    }

    public Task UpdateAsync(IAccount account, ICredit credit, CancellationToken cancellationToken = default)
    {
        _context.AddCommand(() => _dbSetCredit.InsertOneAsync(
            (Credit)credit,
            cancellationToken: cancellationToken));

        _context.AddCommand(() => _dbSetAccount.ReplaceOneAsync(
            f => f.Id == account.Id,
            (Account)account,
            cancellationToken: cancellationToken));

        return Task.CompletedTask;
    }

    public Task UpdateAsync(IAccount account, IDebit debit, CancellationToken cancellationToken = default)
    {
        _context.AddCommand(() => _dbSetDebit.InsertOneAsync(
            (Debit)debit,
            cancellationToken: cancellationToken));

        _context.AddCommand(() => _dbSetAccount.ReplaceOneAsync(
            f => f.Id == account.Id,
            (Account)account,
            cancellationToken: cancellationToken));

        return Task.CompletedTask;
    }
}
