using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public sealed class CloseAccountInput : IInputType
{
    public Guid AccountId { get; }

    public CloseAccountInput(in Guid accountId)
    {
        if (accountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
        }

        AccountId = accountId;
    }
}