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

    public Task Add(IAccount account, ICredit credit)
    {
        _context.AddCommand(() => _dbSetAccount.InsertOneAsync((Account)account));
        _context.AddCommand(() => _dbSetCredit.InsertOneAsync((Credit)credit));
        return Task.CompletedTask;
    }

    public async Task Delete(IAccount account) => _ = await _dbSetAccount.DeleteOneAsync(d => d.Id == account.Id);

    public async Task<IAccount?> Get(Guid id)
    {
        var accounts = await _dbSetAccount.FindAsync(f => f.Id == id);
        var account = accounts.FirstOrDefault();

        if (account == null)
        {
            return null;
        }

        var credits = await _dbSetCredit.FindAsync(f => f.AccountId == account.Id);
        var debits = await _dbSetDebit.FindAsync(f => f.AccountId == account.Id);

        account.Load(credits.ToList(), debits.ToList());

        return account;
    }

    public async Task Update(IAccount account, ICredit credit)
        => await _dbSetCredit.FindOneAndReplaceAsync(f => f.Id == credit.Id, (Credit)credit);

    public async Task Update(IAccount account, IDebit debit)
        => await _dbSetDebit.FindOneAndReplaceAsync(f => f.Id == debit.Id, (Debit)debit);

}
