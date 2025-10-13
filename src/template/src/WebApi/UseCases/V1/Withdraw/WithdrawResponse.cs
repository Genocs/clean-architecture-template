using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Withdraw;

public sealed class WithdrawResponse
{
    [Required]
    public decimal Amount { get; }

    [Required]
    public string Description { get; }

    [Required]
    public DateTime TransactionDate { get; }

    [Required]
    public decimal UpdateBalance { get; }

    public WithdrawResponse(decimal amount, string description, DateTime transactionDate, decimal updatedBalance)
    {
        Amount = amount;
        Description = description;
        TransactionDate = transactionDate;
        UpdateBalance = updatedBalance;
    }
}