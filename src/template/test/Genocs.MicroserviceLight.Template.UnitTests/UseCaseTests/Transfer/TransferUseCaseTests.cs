namespace Genocs.MicroserviceLight.Template.UnitTests.UseCaseTests.Transfer
{
    using Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer;
    using Genocs.MicroserviceLight.Template.Application.UseCases;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess;
    using Genocs.MicroserviceLight.Template.UnitTests.TestFixtures;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class TransferUseCaseTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;
        public TransferUseCaseTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(PositiveDataSetup))]
        public async Task Transfer_ChangesBalance_WhenAccountExists(
            decimal amount,
            decimal expectedOriginBalance)
        {
            var presenter = new TransferPresenter();
            var sut = new Transfer(
                _fixture.EntityFactory,
                presenter,
                _fixture.AccountRepository,
                _fixture.UnitOfWork
            );

            await sut.Execute(
                new TransferInput(
                    _fixture.Context.DefaultAccountId,
                    _fixture.Context.SecondAccountId,
                    new PositiveMoney(amount)));

            var actual = presenter.Transfers.Last();
            Assert.Equal(expectedOriginBalance, actual.UpdatedBalance);
        }
    }
}