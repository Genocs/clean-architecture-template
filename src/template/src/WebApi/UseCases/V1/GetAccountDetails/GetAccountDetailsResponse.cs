using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetAccountDetails;

/// <summary>
/// GetAsync Account Details.
/// </summary>
public sealed class GetAccountDetailsResponse(Guid accountId, decimal currentBalance, List<TransactionModel> transactions)
{
    /// <summary>
    /// Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; } = accountId;

    /// <summary>
    /// Current Balance.
    /// </summary>
    [Required]
    public decimal CurrentBalance { get; } = currentBalance;

    /// <summary>
    /// Transactions.
    /// </summary>
    [Required]
    public IList<TransactionModel> Transactions { get; } = transactions;
}