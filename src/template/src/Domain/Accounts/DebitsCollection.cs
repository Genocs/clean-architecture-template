using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public sealed class DebitsCollection
{
    private readonly IList<IDebit> _debits;

    public DebitsCollection()
    {
        _debits = new List<IDebit>();
    }

    public void Add<T>(IEnumerable<T> debits)
        where T : IDebit
    {
        foreach (var debit in debits)
            Add(debit);
    }

    public void Add(IDebit debit)
    {
        _debits.Add(debit);
    }

    public IReadOnlyCollection<IDebit> GetTransactions()
    {
        return new ReadOnlyCollection<IDebit>(_debits);
    }

    public PositiveMoney GetTotal()
    {
        PositiveMoney total = new PositiveMoney(0);

        foreach (var debit in _debits)
        {
            total = debit.Sum(total);
        }

        return total;
    }
}