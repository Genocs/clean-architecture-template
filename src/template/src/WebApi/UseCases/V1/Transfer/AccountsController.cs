using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Transfer;

[FeatureGate(Features.Transfer)]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class AccountsController : ControllerBase
{
    private readonly IUseCase _transferUseCase;
    private readonly TransferPresenter _presenter;

    public AccountsController(
                                IUseCase transferUseCase,
                                TransferPresenter presenter)
    {
        _transferUseCase = transferUseCase ?? throw new ArgumentNullException(nameof(transferUseCase));
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }

    /// <summary>
    /// Transfer to an account.
    /// </summary>
    /// <response code="200">The updated balance.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Error.</response>
    /// <param name="request">The request to Transfer.</param>
    /// <returns>The updated balance.</returns>
    [HttpPatch("Transfer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Transfer([FromBody][Required] TransferRequest request)
    {
        var transferInput = new TransferInput(
                                                request.OriginAccountId,
                                                request.DestinationAccountId,
                                                new PositiveMoney(request.Amount));

        await _transferUseCase.Execute(transferInput);
        return _presenter.ViewModel;
    }
}