namespace Genocs.UnitTests.TestFixtures
{
    using Genocs.Infrastructure.InMemoryDataAccess;
    using Genocs.Infrastructure.InMemoryDataAccess.Repositories;

    public sealed class StandardFixture
    {
        public EntityFactory EntityFactory { get; }
        public GenocsContext Context { get; }
        public AccountRepository AccountRepository { get; }
        public CustomerRepository CustomerRepository { get; }
        public UnitOfWork UnitOfWork { get; }

        public StandardFixture()
        {
            Context = new GenocsContext();
            AccountRepository = new AccountRepository(Context);
            CustomerRepository = new CustomerRepository(Context);
            UnitOfWork = new UnitOfWork(Context);
            EntityFactory = new EntityFactory();
        }
    }
}