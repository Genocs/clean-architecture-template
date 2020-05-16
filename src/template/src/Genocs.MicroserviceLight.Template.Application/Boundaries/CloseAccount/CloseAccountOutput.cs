namespace Genocs.MicroserviceLight.Template.Application.Boundaries.CloseAccount
{
    using Domain.Accounts;
    using System;

    public sealed class CloseAccountOutput
    {
        public Guid AccountId { get; }

        public CloseAccountOutput(IAccount account)
        {
            AccountId = account.Id;
        }
    }
}