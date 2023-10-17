using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetCustomerDetails;

/// <summary>
/// The Customer Details.
/// </summary>
public sealed class GetCustomerDetailsResponse
{
    /// <summary>
    /// Customer ID.
    /// </summary>
    [Required]
    public Guid CustomerId { get; }

    /// <summary>
    /// The Social Security Number.
    /// </summary>
    [Required]
    public string SSN { get; }

    /// <summary>
    /// The name.
    /// </summary>
    [Required]
    public string Name { get; }

    /// <summary>
    /// Accounts.
    /// </summary>
    [Required]
    public IList<AccountDetailsModel> Accounts { get; }

    public GetCustomerDetailsResponse(Guid customerId, string ssn, string name, List<AccountDetailsModel> accounts)
    {
        CustomerId = customerId;
        SSN = ssn;
        Name = name;
        Accounts = accounts;
    }
}