using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.UseCases;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;
using Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Transfers;

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
            _fixture.UnitOfWork,
            _fixture.ServiceBus
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