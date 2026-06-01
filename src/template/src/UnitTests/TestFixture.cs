using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;
#if InMemory
using Microsoft.EntityFrameworkCore;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;
#endif
#if SQLServer
using Microsoft.EntityFrameworkCore;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.SQLServer;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.SQLServer.Repositories;
#endif
#if MongoDb
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;
using Microsoft.Extensions.Configuration;
#endif

namespace Genocs.CleanArchitecture.Template.UnitTests;

public sealed class TestFixture
{
    public TestFixture()
    {
#if InMemory
        InMemoryFixture();
#endif
#if SQLServer
        SQLServerFixture();
#endif
#if MongoDb
        MongoDbFixture();
#endif
    }

    public IEntityFactory EntityFactory { get; private set; }
    public GenocsContext Context { get; private set; }
    public IAccountRepository AccountRepository { get; private set; }
    public ICustomerRepository CustomerRepository { get; private set; }
    public IUnitOfWork UnitOfWork { get; private set; }
    public IServiceBusClient ServiceBus { get; private set; }
#if InMemory
    private void InMemoryFixture()
    {
        var options = new DbContextOptionsBuilder<GenocsContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;

        Context = new GenocsContext(options);
        AccountRepository = new AccountRepository(Context);
        CustomerRepository = new CustomerRepository(Context);
        UnitOfWork = new UnitOfWork(Context);
        EntityFactory = new EntityFactory();
        ServiceBus = new FakeServiceBus();
    }
#endif
#if SQLServer
    private void SQLServerFixture()
    {
        var options = new DbContextOptionsBuilder<GenocsContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;

        Context = new GenocsContext(options);
        AccountRepository = new AccountRepository(Context);
        CustomerRepository = new CustomerRepository(Context);
        UnitOfWork = new UnitOfWork(Context);
        EntityFactory = new EntityFactory();
        ServiceBus = new FakeServiceBus();
    }
#endif
#if MongoDb
    private void MongoDbFixture()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        GenocsContext.RegisterConventions();

        Context = new GenocsContext(configuration);
        AccountRepository = new AccountRepository(Context);
        CustomerRepository = new CustomerRepository(Context);
        UnitOfWork = new UnitOfWork(Context);
        EntityFactory = new EntityFactory();
        ServiceBus = new FakeServiceBus();
    }
#endif

    public static TestFixture Instance { get; } = new TestFixture();
}
