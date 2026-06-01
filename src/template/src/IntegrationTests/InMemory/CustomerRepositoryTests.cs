using Genocs.CleanArchitecture.Template.Domain.Customers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Genocs.CleanArchitecture.Template.IntegrationTests.InMemory;

public sealed class CustomerRepositoryTests
{
    private readonly EntityFactory _entityFactory = new();

    [Fact]
    public async Task Add_ChangesDatabaseAsync()
    {
        string databaseName = Guid.NewGuid().ToString();

        var customer = _entityFactory.NewCustomer(
            new SSN("8608179992"),
            new Name("Nocco Vincenzo"));

        await using var context = BuildContext(databaseName);
        context.Database.EnsureCreated();

        var repository = new CustomerRepository(context);

        await repository.AddAsync(customer);
        await context.SaveChangesAsync();

        Assert.Single(context.Customers);
        Assert.Contains(context.Customers, c => c.Id == customer.Id);
    }

    [Fact]
    public async Task Get_ReturnsCustomerAsync()
    {
        string databaseName = Guid.NewGuid().ToString();

        var customer1 = _entityFactory.NewCustomer(
            new SSN("8608179994"),
            new Name("Nocco Roberto"));

        await using (var writeContext = BuildContext(databaseName))
        {
            writeContext.Database.EnsureCreated();
            var writeRepository = new CustomerRepository(writeContext);
            await writeRepository.AddAsync(customer1);
            await writeContext.SaveChangesAsync();
        }

        await using var readContext = BuildContext(databaseName);
        var readRepository = new CustomerRepository(readContext);
        ICustomer? customer2 = await readRepository.GetAsync(customer1.Id);

        Assert.NotNull(customer2);
    }

    private GenocsContext BuildContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<GenocsContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new GenocsContext(options);
    }
}