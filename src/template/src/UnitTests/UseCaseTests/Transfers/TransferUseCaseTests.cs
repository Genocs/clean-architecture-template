using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.UseCases;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Transfers;

public sealed class TransferUseCaseTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PositiveDataSetup))]
    public async Task Transfer_ChangesBalance_WhenAccountExistsAsync(
        decimal amount,
        decimal expectedOriginBalance)
    {
        // Arrange - Create and persist origin customer with account
        var originCustomer = _fixture.EntityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Origin Customer"));

        var originAccount = _fixture.EntityFactory.NewAccount(originCustomer);

        // Add initial deposit to origin account (must have enough balance for transfer)
        decimal originInitialAmount = amount + expectedOriginBalance; // Ensure sufficient balance
        var originInitialCredit = originAccount.Deposit(_fixture.EntityFactory, new PositiveMoney(originInitialAmount));

        originCustomer.Register(originAccount);

        // Create and persist destination customer with account
        var destinationCustomer = _fixture.EntityFactory.NewCustomer(
            new SSN("198608179933"),
            new Name("Destination Customer"));

        var destinationAccount = _fixture.EntityFactory.NewAccount(destinationCustomer);

        // Add initial deposit to destination account
        var destinationInitialCredit = destinationAccount.Deposit(_fixture.EntityFactory, new PositiveMoney(100m));

        destinationCustomer.Register(destinationAccount);

        // Persist both customers and accounts to the in-memory database
        await _fixture.CustomerRepository.AddAsync(originCustomer);
        await _fixture.AccountRepository.AddAsync(originAccount, originInitialCredit);
        await _fixture.CustomerRepository.AddAsync(destinationCustomer);
        await _fixture.AccountRepository.AddAsync(destinationAccount, destinationInitialCredit);
        await _fixture.UnitOfWork.SaveAsync();

        var presenter = new TransferPresenter();
        var sut = new Transfer(
            _fixture.EntityFactory,
            presenter,
            _fixture.AccountRepository,
            _fixture.UnitOfWork,
            _fixture.ServiceBus);

        // Act
        await sut.ExecuteAsync(
            new TransferInput(
                originAccount.Id,
                destinationAccount.Id,
                new PositiveMoney(amount)));

        // Assert
        var actual = presenter.Transfers.Last();
        Assert.Equal(expectedOriginBalance, actual.UpdatedBalance);
    }
}