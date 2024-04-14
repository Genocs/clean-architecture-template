using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Register;

[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class CustomersController : ControllerBase
{
    private readonly IUseCase _registerUseCase;
    private readonly RegisterPresenter _presenter;

    public CustomersController(
                                IUseCase registerUseCase,
                                RegisterPresenter presenter)
    {
        _registerUseCase = registerUseCase ?? throw new ArgumentNullException(nameof(registerUseCase));
        _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }

    /// <summary>
    /// Register a customer.
    /// </summary>
    /// <response code="200">The registered customer was create successfully.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Error.</response>
    /// <param name="request">The request to register a customer.</param>
    /// <returns>The newly registered customer.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> Post([FromBody][Required] RegisterRequest request)
    {
        var registerInput = new RegisterInput(
                                                new SSN(request.SSN),
                                                new Name(request.Name),
                                                new PositiveMoney(request.InitialAmount));

        await _registerUseCase.Execute(registerInput);

        return _presenter.ViewModel;
    }
}