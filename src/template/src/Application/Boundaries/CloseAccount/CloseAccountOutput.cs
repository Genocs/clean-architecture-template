using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public sealed class CloseAccountOutput
{
    public Guid AccountId { get; }

    public CloseAccountOutput(IAccount account)
    {
        AccountId = account.Id;
    }
}