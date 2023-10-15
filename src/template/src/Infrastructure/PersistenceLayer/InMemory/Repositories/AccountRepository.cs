namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory.Repositories
{
    using Application.Repositories;
    using Domain.Accounts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class AccountRepository : IAccountRepository
    {
        private readonly GenocsContext _context;

        public AccountRepository(GenocsContext context)
        {
            _context = context;
        }

        public async Task Add(IAccount account, ICredit credit)
        {
            _context.Accounts.Add((InMemory.Account)account);
            _context.Credits.Add((InMemory.Credit)credit);
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
            Account account = _context.Accounts
                .Where(e => e.Id == id)
                .SingleOrDefault();

            return await Task.FromResult<Account>(account);
        }

        public async Task Update(IAccount account, ICredit credit)
        {
            Account accountOld = _context.Accounts
                .Where(e => e.Id == account.Id)
                .SingleOrDefault();

            accountOld = (Account)account;
            await Task.CompletedTask;
        }

        public async Task Update(IAccount account, IDebit debit)
        {
            Account accountOld = _context.Accounts
                .Where(e => e.Id == account.Id)
                .SingleOrDefault();

            accountOld = (Account)account;
            await Task.CompletedTask;
        }
    }
}