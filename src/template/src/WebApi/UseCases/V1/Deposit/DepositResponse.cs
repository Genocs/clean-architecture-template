using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Deposit;

/// <summary>
/// The response for a successful Deposit.
/// </summary>
public sealed class DepositResponse(decimal amount, string description, DateTime transactionDate, decimal updatedBalance)
{
    /// <summary>
    /// Amount Deposited.
    /// </summary>
    [Required]
    public decimal Amount { get; } = amount;

    /// <summary>
    /// Description.
    /// </summary>
    [Required]
    public string Description { get; } = description;

    /// <summary>
    /// Transaction Date.
    /// </summary>
    [Required]
    public DateTime TransactionDate { get; } = transactionDate;

    /// <summary>
    /// Updated Balance.
    /// </summary>
    [Required]
    public decimal UpdateBalance { get; } = updatedBalance;
}