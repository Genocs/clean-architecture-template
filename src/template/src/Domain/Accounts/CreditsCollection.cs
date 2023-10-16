using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public sealed class CreditsCollection
{
    private readonly IList<ICredit> _credits;

    public CreditsCollection()
    {
        _credits = new List<ICredit>();
    }

    public void Add<T>(IEnumerable<T> credits)
        where T : ICredit
    {
        foreach (var credit in credits)
            Add(credit);
    }

    public void Add(ICredit credit)
    {
        _credits.Add(credit);
    }

    public IReadOnlyCollection<ICredit> GetTransactions()
    {
        var transactions = new ReadOnlyCollection<ICredit>(_credits);
        return transactions;
    }

    public PositiveMoney GetTotal()
    {
        PositiveMoney total = new PositiveMoney(0);

        foreach (var credit in _credits)
        {
            total = credit.Sum(total);
        }

        return total;
    }
}