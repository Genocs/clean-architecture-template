using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public sealed class CreditsCollection
{
    private readonly IList<ICredit> _credits;

    public CreditsCollection()
    {
        _credits = [];
    }

    public CreditsCollection(IEnumerable<ICredit> credits)
    {
        _credits = [];
        Add(credits);
    }

    public IReadOnlyCollection<ICredit> Credits
        => new ReadOnlyCollection<ICredit>(_credits);

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
        => new ReadOnlyCollection<ICredit>(_credits);

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