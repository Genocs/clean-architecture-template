using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;

namespace Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;

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