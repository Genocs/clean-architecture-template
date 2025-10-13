using Genocs.CleanArchitecture.Template.Application.Exceptions;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;

public sealed class GetAccountDetailsInput
{
    public Guid AccountId { get; }

    public GetAccountDetailsInput(in Guid accountId)
    {
        if (accountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
        }

        AccountId = accountId;
    }
}