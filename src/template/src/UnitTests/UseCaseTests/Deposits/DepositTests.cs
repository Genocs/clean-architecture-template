using Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;
using Genocs.CleanArchitecture.Template.Application.UseCases;
using Genocs.CleanArchitecture.Template.Domain.Exceptions;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Deposits;

public sealed class DepositTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(PositiveDataSetup))]
    public async Task Deposit_ChangesBalanceAsync(decimal amount)
    {
        // Arrange - Create and persist customer with account
        var customer = _fixture.EntityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var account = _fixture.EntityFactory.NewAccount(customer);

        // Add an initial deposit to the account so it can be persisted
        var initialCredit = account.Deposit(_fixture.EntityFactory, new PositiveMoney(100m));

        customer.Register(account);

        // Persist customer and account to the in-memory database
        await _fixture.CustomerRepository.AddAsync(customer);
        await _fixture.AccountRepository.AddAsync(account, initialCredit);
        await _fixture.UnitOfWork.SaveAsync();

        var presenter = new DepositPresenter();

        var sut = new Deposit(
            _fixture.EntityFactory,
            presenter,
            _fixture.AccountRepository,
            _fixture.UnitOfWork,
            _fixture.ServiceBus);

        // Act
        await sut.ExecuteAsync(
            new DepositInput(
                account.Id,
                new PositiveMoney(amount)));

        var output = presenter.Deposits.Last();

        // Assert
        Assert.Equal(amount, output.Transaction.Amount);
    }

    [Theory]
    [ClassData(typeof(NegativeDataSetup))]
    public async Task Deposit_ShouldNot_ChangesBalance_WhenNegativeAsync(decimal amount)
    {
        // Arrange
        var customer = _fixture.EntityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var account = _fixture.EntityFactory.NewAccount(customer);

        var presenter = new DepositPresenter();

        var sut = new Deposit(
            _fixture.EntityFactory,
            presenter,
            _fixture.AccountRepository,
            _fixture.UnitOfWork,
            _fixture.ServiceBus);

        // Act & Assert
        await Assert.ThrowsAsync<MoneyShouldBePositiveException>(() =>
            sut.ExecuteAsync(new DepositInput(account.Id, new PositiveMoney(amount))));
    }
}