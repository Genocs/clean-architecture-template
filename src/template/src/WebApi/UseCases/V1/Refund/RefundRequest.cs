namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Refund
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Withdraw Request
    /// </summary>
    public class RefundRequest
    {
        /// <summary>
        /// The Account ID
        /// </summary>
        /// <value></value>
        [Required]
        public Guid AccountId { get; set; }

        /// <summary>
        /// The amount to withdraw
        /// </summary>
        [Required]
        public decimal Amount { get; set; }
    }
}