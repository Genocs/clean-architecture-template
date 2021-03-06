namespace Genocs.MicroserviceLight.Template.UnitTests.UseCasesTests.Register
{
    using Application.Boundaries.Register;
    using Application.UseCases;
    using Domain.ValueObjects;
    using Infrastructure.PersistenceLayer.InMemory;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UnitTests.TestFixtures;
    using Xunit;

    public sealed class RegisterTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;
        public RegisterTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var register = new Register(null, null, null, null, null, null);
            Assert.ThrowsAsync<Exception>(async () => await register.Execute(null));
        }

        [Theory]
        [ClassData(typeof(PositiveDataSetup))]
        public async Task Register_WritesOutput_InputIsValid(decimal amount)
        {
            var presenter = new RegisterPresenter();
            var ssn = new SSN("8608178888");
            var name = new Name("Nocco Giovanni Emanuele");

            var sut = new Register(
                _fixture.EntityFactory,
                presenter,
                _fixture.CustomerRepository,
                _fixture.AccountRepository,
                _fixture.UnitOfWork,
                _fixture.ServiceBus
            );

            await sut.Execute(new RegisterInput(
                ssn,
                name,
                new PositiveMoney(amount)));

            var actual = presenter.Registers.Last();
            Assert.NotNull(actual);
            Assert.Equal(ssn.ToString(), actual.Customer.SSN);
            Assert.Equal(name.ToString(), actual.Customer.Name);
            Assert.Equal(amount, actual.Account.CurrentBalance);
        }
    }
}