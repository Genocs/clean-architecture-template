namespace Genocs.MicroserviceLight.Template.Application.Boundaries.GetAccountDetails
{
    using Genocs.MicroserviceLight.Template.Application.Exceptions;
    using System;

    public sealed class GetAccountDetailsInput
    {
        public Guid AccountId { get; }

        public GetAccountDetailsInput(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
            }

            AccountId = accountId;
        }
    }
}