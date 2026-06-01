using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.CloseAccount;

/// <summary>
/// The CloseAsync Account Request.
/// </summary>
public sealed class CloseAccountRequest
{
    /// <summary>
    /// Account ID.
    /// </summary>
    [Required]
    public Guid AccountId { get; set; }
}