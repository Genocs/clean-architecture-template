using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Deposit;

/// <summary>
/// The response for a successful Deposit.
/// </summary>
public sealed class DepositResponse
{
    /// <summary>
    /// Amount Deposited.
    /// </summary>
    [Required]
    public decimal Amount { get; }

    /// <summary>
    /// Description.
    /// </summary>
    [Required]
    public string Description { get; }

    /// <summary>
    /// Transaction Date.
    /// </summary>
    [Required]
    public DateTime TransactionDate { get; }

    /// <summary>
    /// Updated Balance.
    /// </summary>
    [Required]
    public decimal UpdateBalance { get; }

    public DepositResponse(
                            decimal amount,
                            string description,
                            DateTime transactionDate,
                            decimal updatedBalance)
    {
        Amount = amount;
        Description = description;
        TransactionDate = transactionDate;
        UpdateBalance = updatedBalance;
    }
}