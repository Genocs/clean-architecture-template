using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.SQLServer;

public class CustomerTests
{
    [Fact]
    public void Customer_Should_Be_Registered_With_1_Account()
    {
        var entityFactory = TestFixture.Instance.EntityFactory;

        // Arrange
        var sut = entityFactory.NewCustomer(
            new SSN("8608179800"),
            new Name("Nocco Pantaleo"));

        var account = entityFactory.NewAccount(sut);

        // Act
        sut.Register(account);

        // Assert
        Assert.Single(sut.Accounts.GetAccountIds());
    }
}