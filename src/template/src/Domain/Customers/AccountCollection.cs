using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Domain.Customers;

public sealed class AccountCollection
{
    private readonly IList<Guid> _accountIds;

    public AccountCollection()
    {
        _accountIds = new List<Guid>();
    }

    public void Add(IEnumerable<Guid> accounts)
    {
        foreach (var account in accounts)
            Add(account);
    }

    public IReadOnlyCollection<Guid> GetAccountIds()
    {
        IReadOnlyCollection<Guid> accountIds = new ReadOnlyCollection<Guid>(_accountIds);
        return accountIds;
    }

    public void Add(Guid accountId)
    {
        _accountIds.Add(accountId);
    }
}