using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.Common.Domain.Entities;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public interface IAccount : IAggregateRoot<Guid>
{
    ICredit Deposit(IEntityFactory entityFactory, PositiveMoney amountToDeposit);
    IDebit? Withdraw(IEntityFactory entityFactory, PositiveMoney amountToWithdraw);
    bool IsClosingAllowed();
    Money GetCurrentBalance();
}