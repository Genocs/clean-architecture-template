using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework;

public class Credit : Domain.Accounts.Credit
{
    public Guid AccountId { get; protected set; }

    protected Credit() { }

    public Credit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
    {
        AccountId = account.Id;
        this.Amount = amountToDeposit;
        this.TransactionDate = transactionDate;
    }
}