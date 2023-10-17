using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Deposit;

/// <summary>
/// The request to Deposit.
/// </summary>
public sealed class DepositRequest
{
    /// <summary>
    /// The Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }

    /// <summary>
    /// The amount to Deposit.
    /// </summary>
    [Required]
    public decimal Amount { get; set; }
}