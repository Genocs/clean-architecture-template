namespace Genocs.UnitTests.UseCasesTests.Deposit
{
    using Application.Boundaries.Deposit;
    using Genocs.Application.UseCases;
    using Genocs.Domain.ValueObjects;
    using Genocs.Infrastructure.InMemoryDataAccess;
    using Genocs.UnitTests.TestFixtures;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class DepositTests : IClassFixture<StandardFixture>
    {
        private readonly StandardFixture _fixture;
        public DepositTests(StandardFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [ClassData(typeof(PositiveDataSetup))]
        public async Task Deposit_ChangesBalance(decimal amount)
        {
            var presenter = new DepositPresenter();
            var sut = new Deposit(
                _fixture.EntityFactory,
                presenter,
                _fixture.AccountRepository,
                _fixture.UnitOfWork
            );

            await sut.Execute(
                new DepositInput(
                    _fixture.Context.DefaultAccountId,
                    new PositiveMoney(amount)));

            var output = presenter.Deposits.Last();
            Assert.Equal(amount, output.Transaction.Amount);
        }

        [Theory]
        [ClassData(typeof(NegativeDataSetup))]
        public async Task Deposit_ShouldNot_ChangesBalance_WhenNegative(decimal amount)
        {
            var presenter = new DepositPresenter();
            var sut = new Deposit(
                _fixture.EntityFactory,
                presenter,
                _fixture.AccountRepository,
                _fixture.UnitOfWork
            );

            await Assert.ThrowsAsync<MoneyShouldBePositiveException>(() =>
                sut.Execute(
                    new DepositInput(
                        _fixture.Context.DefaultAccountId,
                        new PositiveMoney(amount)
                    )));
        }
    }
}