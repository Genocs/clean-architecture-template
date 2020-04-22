namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Deposit
{
    using Genocs.MicroserviceLight.Template.Application.Exceptions;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using System;

    public sealed class DepositInput
    {
        public Guid AccountId { get; }
        public PositiveMoney Amount { get; }

        public DepositInput(Guid accountId, PositiveMoney amount)
        {
            if (accountId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
            }

            if (amount == null)
            {
                throw new InputValidationException($"{nameof(amount)} cannot be null.");
            }

            this.AccountId = accountId;
            this.Amount = amount;
        }
    }
}