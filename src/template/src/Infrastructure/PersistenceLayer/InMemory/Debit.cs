using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;

public class Debit : Domain.Accounts.Debit
{
    public Guid AccountId { get; protected set; }

    protected Debit()
    {
    }

    public Debit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
    {
        AccountId = account.Id;
        Amount = amountToWithdraw;
        TransactionDate = transactionDate;
    }
}