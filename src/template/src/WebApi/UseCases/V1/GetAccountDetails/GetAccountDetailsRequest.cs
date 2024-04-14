using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetAccountDetails;

/// <summary>
/// The Get Account Details Request.
/// </summary>
public sealed class GetAccountDetailsRequest
{
    /// <summary>
    /// Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }
}