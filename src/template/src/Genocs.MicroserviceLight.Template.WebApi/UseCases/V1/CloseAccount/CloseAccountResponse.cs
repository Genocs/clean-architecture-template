namespace Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.CloseAccount
{
    using Application.Boundaries.CloseAccount;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Close Account Response
    /// </summary>
    public sealed class CloseAccountResponse
    {
        /// <summary>
        /// Account ID
        /// </summary>
        [Required]
        public Guid AccountId { get; }

        public CloseAccountResponse(CloseAccountOutput output)
        {
            AccountId = output.AccountId;
        }
    }
}