namespace Genocs.Domain.Accounts
{
    using Genocs.Domain.ValueObjects;

    public interface IAccount : IAggregateRoot
    {
        ICredit Deposit(IEntityFactory entityFactory, PositiveMoney amountToDeposit);
        IDebit Withdraw(IEntityFactory entityFactory, PositiveMoney amountToWithdraw);
        bool IsClosingAllowed();
        Money GetCurrentBalance();
    }
}