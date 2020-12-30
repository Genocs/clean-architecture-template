namespace Genocs.MicroserviceLight.Template.UnitTests.UseCasesTests.CloseAccount
{
    using Domain.ValueObjects;
    using UnitTests.TestFixtures;
    using Xunit;

    public sealed class CloseAccountTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;
        public CloseAccountTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(PositiveDataSetup))]
        public void PositiveBalance_Should_Not_Allow_Closing(decimal amount)
        {
            var customer = _fixture.EntityFactory.NewCustomer(
                new SSN("198608178899"),
                new Name("Nocco Giovanni Emanuele")
            );

            var account = _fixture.EntityFactory.NewAccount(customer);

            account.Deposit(_fixture.EntityFactory, new PositiveMoney(amount));

            bool actual = account.IsClosingAllowed();

            Assert.False(actual);
        }

        [Fact]
        public void ZeroBalance_Should_Allow_Closing()
        {
            var customer = _fixture.EntityFactory.NewCustomer(
                new SSN("198608178899"),
                new Name("Nocco Giovanni Emanuele")
            );

            var account = _fixture.EntityFactory.NewAccount(customer);
            bool actual = account.IsClosingAllowed();

            Assert.True(actual);
        }
    }
}