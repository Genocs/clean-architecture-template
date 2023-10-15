using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public sealed class RefundInput
{
    public Guid AccountId { get; }
    public PositiveMoney Amount { get; }

    public RefundInput(Guid accountId, PositiveMoney amount)
    {
        if (accountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
        }

        if (amount == null)
        {
            throw new InputValidationException($"{nameof(amount)} cannot be null.");
        }

        AccountId = accountId;
        Amount = amount;
    }
}