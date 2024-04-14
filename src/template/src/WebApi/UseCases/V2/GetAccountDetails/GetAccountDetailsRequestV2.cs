using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V2.GetAccountDetails;

/// <summary>
/// The Get Account Details Request.
/// </summary>
public sealed class GetAccountDetailsRequestV2
{
    /// <summary>
    /// Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }
}