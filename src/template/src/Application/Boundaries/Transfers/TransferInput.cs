using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;

public sealed class TransferInput
{
    public Guid OriginAccountId { get; }
    public Guid DestinationAccountId { get; }
    public PositiveMoney Amount { get; }

    public TransferInput(Guid originAccountId, Guid destinationAccountId, PositiveMoney amount)
    {
        if (originAccountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(originAccountId)} cannot be empty.");
        }

        if (destinationAccountId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(destinationAccountId)} cannot be empty.");
        }

        if (amount == null)
        {
            throw new InputValidationException($"{nameof(amount)} cannot be null.");
        }

        OriginAccountId = originAccountId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
    }
}