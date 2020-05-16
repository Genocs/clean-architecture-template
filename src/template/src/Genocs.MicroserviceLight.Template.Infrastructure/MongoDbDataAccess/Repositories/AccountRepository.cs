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
        private readonly GenocsContext _context;

        public AccountRepository(GenocsContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(IAccount account, ICredit credit)
        {
            await _context.Accounts.InsertOneAsync((MongoDbDataAccess.Account)account);
            await _context.Credits.InsertOneAsync((MongoDbDataAccess.Credit)credit);
        }

        public async Task Delete(IAccount account)
        {
            await _context.Accounts.DeleteOneAsync(d => d.Id == account.Id);
        }

        public async Task<IAccount> Get(Guid id)
        {
            var accounts = await _context.GetCollection<MongoDbDataAccess.Account>("Accounts").FindAsync(f => f.Id == id);
            MongoDbDataAccess.Account account = accounts.FirstOrDefault();

            if (account == null)
                return null;

            var credits = await _context.GetCollection<MongoDbDataAccess.Credit>("Credits").FindAsync(f => f.AccountId == account.Id);
            var debits = await _context.GetCollection<MongoDbDataAccess.Debit>("Debits").FindAsync(f => f.AccountId == account.Id);

            account.Load(credits.ToList(), debits.ToList());

            return account;
        }

        public async Task Update(IAccount account, ICredit credit)
            => await _context.Credits.FindOneAndReplaceAsync(f => f.Id == credit.Id, (MongoDbDataAccess.Credit)credit);

        public async Task Update(IAccount account, IDebit debit)
            => await _context.Debits.FindOneAndReplaceAsync(f => f.Id == debit.Id, (MongoDbDataAccess.Debit)debit);

    }
}
