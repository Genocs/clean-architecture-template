using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetCustomerDetails;

/// <summary>
/// The Customer Details.
/// </summary>
public sealed class GetCustomerDetailsResponse(Guid customerId, string ssn, string name, List<AccountDetailsModel> accounts)
{
    /// <summary>
    /// Customer ID.
    /// </summary>
    [Required]
    public Guid CustomerId { get; } = customerId;

    /// <summary>
    /// The Social Security Number.
    /// </summary>
    [Required]
    public string SSN { get; } = ssn;

    /// <summary>
    /// The name.
    /// </summary>
    [Required]
    public string Name { get; } = name;

    /// <summary>
    /// Accounts.
    /// </summary>
    [Required]
    public IList<AccountDetailsModel> Accounts { get; } = accounts;
}