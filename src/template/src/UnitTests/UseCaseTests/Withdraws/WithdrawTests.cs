using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using Genocs.CleanArchitecture.Template.Application.UseCases;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;
using Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Withdraws;

public sealed class WithdrawlTests : IClassFixture<StandardFixture>
{
    private readonly StandardFixture _fixture;
    public WithdrawlTests(StandardFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [ClassData(typeof(PositiveDataSetup))]
    public async Task Withdraw_Valid_Amount(
        decimal amount,
        decimal expectedBalance)
    {
        var presenter = new WithdrawPresenter();
        var sut = new Withdraw(
            _fixture.EntityFactory,
            presenter,
            _fixture.AccountRepository,
            _fixture.UnitOfWork,
            _fixture.ServiceBus
        );

        await sut.Execute(new WithdrawInput(
            _fixture.Context.DefaultAccountId,
            new PositiveMoney(amount)));

        var actual = presenter.Withdrawals.Last();
        Assert.Equal(expectedBalance, actual.UpdatedBalance);
    }
}