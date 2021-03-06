namespace Genocs.MicroserviceLight.Template.UnitTests.TestFixtures
{
    using Application.Services;
    using Infrastructure.PersistenceLayer.InMemory;
    using Infrastructure.PersistenceLayer.InMemory.Repositories;

    public sealed class StandardFixture
    {
        public EntityFactory EntityFactory { get; }
        public GenocsContext Context { get; }
        public AccountRepository AccountRepository { get; }
        public CustomerRepository CustomerRepository { get; }
        public UnitOfWork UnitOfWork { get; }

        public IServiceBusClient ServiceBus { get; }

        public StandardFixture()
        {
            Context = new GenocsContext();
            AccountRepository = new AccountRepository(Context);
            CustomerRepository = new CustomerRepository(Context);
            UnitOfWork = new UnitOfWork(Context);
            EntityFactory = new EntityFactory();
            ServiceBus = new FakeServiceBus();
        }
    }
}