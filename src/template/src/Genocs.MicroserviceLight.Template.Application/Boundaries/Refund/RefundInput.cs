namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Refund
{
    using Application.Exceptions;
    using Domain.ValueObjects;
    using System;

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
}