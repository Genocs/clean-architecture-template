using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetAccountDetails;

/// <summary>
/// Get Account Details.
/// </summary>
public sealed class GetAccountDetailsResponse
{
    /// <summary>
    /// Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; }

    /// <summary>
    /// Current Balance.
    /// </summary>
    [Required]
    public decimal CurrentBalance { get; }

    /// <summary>
    /// Transactions.
    /// </summary>
    [Required]
    public IList<TransactionModel> Transactions { get; }

    public GetAccountDetailsResponse(Guid accountId, decimal currentBalance, List<TransactionModel> transactions)
    {
        AccountId = accountId;
        CurrentBalance = currentBalance;
        Transactions = transactions;
    }
}