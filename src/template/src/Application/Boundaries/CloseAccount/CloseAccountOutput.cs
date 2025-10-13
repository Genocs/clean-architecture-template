using Genocs.CleanArchitecture.Template.Domain.Accounts;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public sealed class CloseAccountOutput(IAccount account)
{
    public Guid AccountId { get; } = account.Id;
}