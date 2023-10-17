using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Refund;

/// <summary>
/// Withdraw Request.
/// </summary>
public class RefundRequest
{
    /// <summary>
    /// The Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount to withdraw.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }
}