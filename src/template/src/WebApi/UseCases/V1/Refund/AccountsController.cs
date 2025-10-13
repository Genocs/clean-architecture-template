using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Refund;

[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class AccountsController : ControllerBase
{
    private readonly IUseCase _refundUseCase;
    private readonly RefundPresenter _presenter;

    public AccountsController(
                                IUseCase refundUseCase,
                                RefundPresenter presenter)
    {
        _refundUseCase = refundUseCase ?? throw new ArgumentNullException(nameof(refundUseCase));
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }

    /// <summary>
    /// Refund on an account.
    /// </summary>
    /// <response code="200">The updated balance.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Error.</response>
    /// <param name="request">The request to Refund.</param>
    /// <returns>The updated balance.</returns>
    [HttpPatch("Refund")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefundResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> Refund([FromBody][Required] RefundRequest request)
    {
        RefundInput refundInput = new RefundInput(
                                                    request.AccountId,
                                                    new PositiveMoney(request.Amount));

        await _refundUseCase.ExecuteAsync(refundInput);
        return _presenter.ViewModel;
    }
}