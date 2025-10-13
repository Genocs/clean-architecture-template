using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V2.GetAccountDetails;

[FeatureGate(Features.GetAccountDetailsV2)]
[ApiVersion("2.0")]
[Route("api/v2/[controller]")]
[ApiController]
public sealed class AccountsV2Controller(IUseCase getAccountDetailsUseCase, GetAccountDetailsPresenterV2 presenter) : ControllerBase
{
    private readonly IUseCase _getAccountDetailsUseCase = getAccountDetailsUseCase;
    private readonly GetAccountDetailsPresenterV2 _presenter = presenter;

    /// <summary>
    /// Get an account details.
    /// </summary>
    [HttpGet("{AccountId}", Name = "GetAccountV2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> Get([FromRoute][Required] GetAccountDetailsRequestV2 request)
    {
        var getAccountDetailsInput = new GetAccountDetailsInput(request.AccountId);
        await _getAccountDetailsUseCase.ExecuteAsync(getAccountDetailsInput);
        return _presenter.ViewModel;
    }
}