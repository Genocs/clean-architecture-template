using Genocs.CleanArchitecture.Template.Application.Exceptions;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public sealed class CloseAccountInput
{
    public Guid AccountId { get; }

    public CloseAccountInput(Guid accountId)
    {
        if (accountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
        }

        AccountId = accountId;
    }
}