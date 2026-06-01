using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Register;

/// <summary>
/// Registration Request.
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    /// SSN.
    /// </summary>
    [Required]
    public required string SSN { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    public required string Name { get; init; }

    /// <summary>
    /// Initial Amount.
    /// </summary>
    [Required]
    public required decimal InitialAmount { get; init; }
}