using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetAccountDetails;
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class AccountsController(IUseCase getAccountDetailsUseCase, GetAccountDetailsPresenter presenter) : ControllerBase
{
    private readonly IUseCase _getAccountDetailsUseCase = getAccountDetailsUseCase;
    private readonly GetAccountDetailsPresenter _presenter = presenter;

    /// <summary>
    /// Get an account details
    /// </summary>
    [HttpGet("{AccountId}", Name = "GetAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> Get([FromRoute][Required] GetAccountDetailsRequest request)
    {
        var getAccountDetailsInput = new GetAccountDetailsInput(request.AccountId);
        await _getAccountDetailsUseCase.ExecuteAsync(getAccountDetailsInput);
        return _presenter.ViewModel;
    }
}