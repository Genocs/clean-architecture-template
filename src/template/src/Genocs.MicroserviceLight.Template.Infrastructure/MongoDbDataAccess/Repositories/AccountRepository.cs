namespace Genocs.MicroserviceLight.Template.Infrastructure.MongoDbDataAccess.Repositories
{
    using Application.Repositories;
    using Domain.Accounts;
    using MongoDB.Driver;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class AccountRepository : IAccountRepository
    {
        private readonly IMongoContext _context;
        private readonly IMongoCollection<MongoDbDataAccess.Account> _DbSetAccount;
        private readonly IMongoCollection<MongoDbDataAccess.Credit> _DbSetCredit;
        private readonly IMongoCollection<MongoDbDataAccess.Debit> _DbSetDebit;

        public AccountRepository(IMongoContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _DbSetAccount = _context.GetCollection<MongoDbDataAccess.Account>("Accounts");
            _DbSetCredit = _context.GetCollection<MongoDbDataAccess.Credit>("Credits");
            _DbSetDebit = _context.GetCollection<MongoDbDataAccess.Debit>("Debits");
        }

        public Task Add(IAccount account, ICredit credit)
        {
            _context.AddCommand(() => _DbSetAccount.InsertOneAsync((MongoDbDataAccess.Account)account));
            _context.AddCommand(() => _DbSetCredit.InsertOneAsync((MongoDbDataAccess.Credit)credit));
            return Task.CompletedTask;
        }

        public async Task Delete(IAccount account)
        {
            await _DbSetAccount.DeleteOneAsync(d => d.Id == account.Id);
        }

        public async Task<IAccount> Get(Guid id)
        {
            var accounts = await _DbSetAccount.FindAsync(f => f.Id == id);
            MongoDbDataAccess.Account account = accounts.FirstOrDefault();

            if (account == null)
                return null;

            var credits = await _DbSetCredit.FindAsync(f => f.AccountId == account.Id);
            var debits = await _DbSetDebit.FindAsync(f => f.AccountId == account.Id);

            account.Load(credits.ToList(), debits.ToList());

            return account;
        }

        public async Task Update(IAccount account, ICredit credit)
            => await _DbSetCredit.FindOneAndReplaceAsync(f => f.Id == credit.Id, (MongoDbDataAccess.Credit)credit);

        public async Task Update(IAccount account, IDebit debit)
            => await _DbSetDebit.FindOneAndReplaceAsync(f => f.Id == debit.Id, (MongoDbDataAccess.Debit)debit);

    }
}
