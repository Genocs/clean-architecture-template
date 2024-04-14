using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Withdraw;

/// <summary>
/// Withdraw Request.
/// </summary>
public class WithdrawRequest
{
    /// <summary>
    /// The Account ID.
    /// </summary>
    /// <value>The unique identifier.</value>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount to withdraw.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }
}