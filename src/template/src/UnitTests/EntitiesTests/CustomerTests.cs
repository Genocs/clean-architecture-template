namespace Genocs.MicroserviceLight.Template.UnitTests.EntitiesTests
{
    using Domain.Customers;
    using Domain.ValueObjects;
    using Xunit;

    public class CustomerTests
    {
        [Fact]
        public void Customer_Should_Be_Registered_With_1_Account()
        {
            var entityFactory = new Infrastructure.PersistenceLayer.InMemory.EntityFactory();
            //
            // Arrange
            ICustomer sut = entityFactory.NewCustomer(
                new SSN("198608179922"),
                new Name("Nocco Giovanni Emanuele")
            );

            var account = entityFactory.NewAccount(sut);

            // Act
            sut.Register(account);

            // Assert
            Assert.Single(sut.Accounts.GetAccountIds());
        }
    }
}