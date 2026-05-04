using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Domain.Customers;

public sealed class AccountCollection
{
    private readonly IList<Guid> _accountIds = [];

    public void Add(IEnumerable<Guid> accounts)
    {
        foreach (var account in accounts)
            Add(account);
    }

    public IReadOnlyCollection<Guid> GetAccountIds()
        => new ReadOnlyCollection<Guid>(_accountIds);

    public void Add(in Guid accountId)
        => _accountIds.Add(accountId);
}