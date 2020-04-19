namespace Genocs.Application.Boundaries.CloseAccount
{
    using Genocs.Domain.Accounts;
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