namespace Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Refund
{
    using Application.Boundaries.Refund;
    using Domain.ValueObjects;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

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
            _refundUseCase = refundUseCase;
            _presenter = presenter;
        }

        /// <summary>
        /// Refund on an account
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
        public async Task<IActionResult> Refund([FromBody][Required] RefundRequest request)
        {
            var refundInput = new RefundInput(
                request.AccountId,
                new PositiveMoney(request.Amount)
            );
            await _refundUseCase.Execute(refundInput);
            return _presenter.ViewModel;
        }
    }
}