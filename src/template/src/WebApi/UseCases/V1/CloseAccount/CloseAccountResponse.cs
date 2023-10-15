using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.CloseAccount;


/// <summary>
/// Close Account Response
/// </summary>
public sealed class CloseAccountResponse
{
    /// <summary>
    /// Account ID
    /// </summary>
    [Required]
    public Guid AccountId { get; }

    public CloseAccountResponse(CloseAccountOutput output)
    {
        AccountId = output.AccountId;
    }
}