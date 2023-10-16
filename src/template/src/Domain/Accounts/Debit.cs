using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public class Debit : IDebit
{
    public Guid Id { get; protected set; }
    public PositiveMoney Amount { get; protected set; } = new PositiveMoney(0);

    public string Description
    {
        get { return "Debit"; }
    }

    public DateTime TransactionDate { get; protected set; }

    public PositiveMoney Sum(PositiveMoney amount)
    {
        return Amount.Add(amount);
    }
}