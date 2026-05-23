using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.CloseAccount;

[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class AccountsController(
    IUseCase<CloseAccountInput> closeAccountUseCase,
    CloseAccountPresenter presenter) : ControllerBase
{
    private readonly IUseCase<CloseAccountInput> _closeAccountUseCase = closeAccountUseCase;
    private readonly CloseAccountPresenter _presenter = presenter;

    /// <summary>
    /// CloseAsync an Account.
    /// </summary>
    /// <response code="200">The closed account id.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Error.</response>
    /// <param name="request">The request to CloseAsync an Account.</param>
    /// <returns>The account id.</returns>
    [HttpDelete("{AccountId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CloseAccountResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> CloseAsync([FromRoute][Required] CloseAccountRequest request)
    {
        var closeAccountInput = new CloseAccountInput(request.AccountId);
        await _closeAccountUseCase.ExecuteAsync(closeAccountInput);
        return _presenter.ViewModel;
    }
}