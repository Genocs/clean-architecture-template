using System.ComponentModel.DataAnnotations;
using Genocs.CleanArchitecture.Template.WebApi.ViewModels;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V2.GetAccountDetails;

/// <summary>
/// GetAsync Account Details Response.
/// </summary>
public sealed class GetAccountDetailsResponseV2
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

    public GetAccountDetailsResponseV2(Guid accountId, decimal currentBalance, List<TransactionModel> transactions)
    {
        AccountId = accountId;
        CurrentBalance = currentBalance;
        Transactions = transactions;
    }
}