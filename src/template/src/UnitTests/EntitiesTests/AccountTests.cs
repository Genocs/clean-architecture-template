using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.EntitiesTests;

public class AccountTests
{
    [Fact]
    public void New_Account_Should_Have_100_Credit_After_Deposit()
    {
        var entityFactory = new Infrastructure.PersistenceLayer.InMemory.EntityFactory();

        // Arrange
        PositiveMoney amount = new PositiveMoney(100.0M);
        var customer = entityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var sut = entityFactory.NewAccount(customer);

        // Act
        Credit actual = (Credit)sut.Deposit(entityFactory, amount);

        // Assert
        Assert.Equal(100, actual.Amount.ToMoney().ToDecimal());
        Assert.Equal("Credit", actual.Description);
    }

    [Fact]
    public void New_Account_With_1000_Balance_Should_Have_900_Credit_After_Withdraw()
    {
        // Arrange
        var entityFactory = new Infrastructure.PersistenceLayer.InMemory.EntityFactory();

        // Arrange
        var customer = entityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var sut = entityFactory.NewAccount(customer);

        sut.Deposit(entityFactory, new PositiveMoney(1000.0M));

        // Act
        sut.Withdraw(entityFactory, new PositiveMoney(100));

        // Assert
        Assert.Equal(900, sut.GetCurrentBalance().ToDecimal());
    }

    [Fact]
    public void New_Account_Should_Allow_Closing()
    {
        var entityFactory = new Infrastructure.PersistenceLayer.InMemory.EntityFactory();

        // Arrange
        var customer = entityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var sut = entityFactory.NewAccount(customer);

        // Act
        bool actual = sut.IsClosingAllowed();

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Account_With_200_Balance_Should_Not_Allow_50000_Withdraw()
    {
        var entityFactory = new Infrastructure.PersistenceLayer.InMemory.EntityFactory();

        // Arrange
        var customer = entityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var sut = entityFactory.NewAccount(customer);

        var credit = sut.Deposit(entityFactory, new PositiveMoney(200));

        // Act
        var actual = sut.Withdraw(entityFactory, new PositiveMoney(5000));

        // Act and Assert
        Assert.Null(actual);
    }

    [Fact]
    public void Account_With_Three_Transactions_Should_Be_Consistent()
    {
        var entityFactory = new Infrastructure.PersistenceLayer.EntityFramework.EntityFactory();

        // Arrange
        var customer = entityFactory.NewCustomer(
            new SSN("198608179922"),
            new Name("Nocco Giovanni Emanuele"));

        var sut = entityFactory.NewAccount(customer);

        sut.Deposit(entityFactory, new PositiveMoney(200));
        sut.Withdraw(entityFactory, new PositiveMoney(100));
        sut.Deposit(entityFactory, new PositiveMoney(50));

        Assert.Equal(2, ((Account)sut).Credits.GetTransactions().Count);
        Assert.Single(((Account)sut).Debits.GetTransactions());
    }
}