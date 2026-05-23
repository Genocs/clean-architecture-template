using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.CloseAccounts;

public sealed class CloseAccountTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PositiveDataSetup))]
    public void PositiveBalance_Should_Not_Allow_Closing(decimal amount)
    {
        var entityFactory = _fixture.EntityFactory;

        var customer = entityFactory.NewCustomer(
            new SSN("198608178899"),
            new Name("Nocco Giovanni Emanuele"));

        var account = entityFactory.NewAccount(customer);

        account.Deposit(entityFactory, new PositiveMoney(amount));

        bool actual = account.IsClosingAllowed();

        Assert.False(actual);
    }

    [Fact]
    public void ZeroBalance_Should_Allow_Closing()
    {
        var entityFactory = _fixture.EntityFactory;

        var customer = entityFactory.NewCustomer(
            new SSN("198608178899"),
            new Name("Nocco Giovanni Emanuele"));

        var account = entityFactory.NewAccount(customer);
        bool actual = account.IsClosingAllowed();

        Assert.True(actual);
    }
}