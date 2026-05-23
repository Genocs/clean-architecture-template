using Asp.Versioning;
using Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetCustomerDetails;

[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[ApiController]
public sealed class CustomersController(IUseCase<GetCustomerDetailsInput> getCustomerDetailsUseCase, GetCustomerDetailsPresenter presenter) : ControllerBase
{
    private readonly IUseCase<GetCustomerDetailsInput> _getCustomerDetailsUseCase = getCustomerDetailsUseCase;
    private readonly GetCustomerDetailsPresenter _presenter = presenter;

    /// <summary>
    /// GetAsync the Customer details.
    /// </summary>
    [HttpGet("{CustomerId}", Name = "GetCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomerDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> GetCustomerAsync([FromRoute][Required] GetCustomerDetailsRequest request)
    {
        var getCustomerDetailsInput = new GetCustomerDetailsInput(request.CustomerId);
        await _getCustomerDetailsUseCase.ExecuteAsync(getCustomerDetailsInput);
        return _presenter.ViewModel;
    }
}