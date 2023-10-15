using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Domain.Accounts;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;



public sealed class AccountRepository : IAccountRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Account> _DbSetAccount;
    private readonly IMongoCollection<Credit> _DbSetCredit;
    private readonly IMongoCollection<Debit> _DbSetDebit;

    public AccountRepository(IMongoContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));

        _DbSetAccount = _context.GetCollection<Account>("Accounts");
        _DbSetCredit = _context.GetCollection<Credit>("Credits");
        _DbSetDebit = _context.GetCollection<Debit>("Debits");
    }

    public Task Add(IAccount account, ICredit credit)
    {
        _context.AddCommand(() => _DbSetAccount.InsertOneAsync((Account)account));
        _context.AddCommand(() => _DbSetCredit.InsertOneAsync((Credit)credit));
        return Task.CompletedTask;
    }

    public async Task Delete(IAccount account) => _ = await _DbSetAccount.DeleteOneAsync(d => d.Id == account.Id);

    public async Task<IAccount> Get(Guid id)
    {
        var accounts = await _DbSetAccount.FindAsync(f => f.Id == id);
        var account = accounts.FirstOrDefault();

        if (account == null)
        {
            return null;
        }

        var credits = await _DbSetCredit.FindAsync(f => f.AccountId == account.Id);
        var debits = await _DbSetDebit.FindAsync(f => f.AccountId == account.Id);

        account.Load(credits.ToList(), debits.ToList());

        return account;
    }

    public async Task Update(IAccount account, ICredit credit)
        => await _DbSetCredit.FindOneAndReplaceAsync(f => f.Id == credit.Id, (Credit)credit);

    public async Task Update(IAccount account, IDebit debit)
        => await _DbSetDebit.FindOneAndReplaceAsync(f => f.Id == debit.Id, (Debit)debit);

}
