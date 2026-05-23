using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public sealed class RefundInput : IInputType
{
    public Guid AccountId { get; }
    public PositiveMoney Amount { get; }

    public RefundInput(Guid accountId, PositiveMoney amount)
    {
        if (accountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
        }

        AccountId = accountId;
        Amount = amount ?? throw new InputValidationException($"{nameof(amount)} cannot be null.");
    }
}