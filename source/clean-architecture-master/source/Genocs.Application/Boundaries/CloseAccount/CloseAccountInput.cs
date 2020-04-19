namespace Genocs.Application.Boundaries.CloseAccount
{
    using Genocs.Application.Exceptions;
    using System;

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
}