using Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Deposit;


public sealed class DepositPresenter : IOutputPort
{
    public IActionResult ViewModel { get; private set; }

    public void Error(string message)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = "An error occurred",
            Detail = message
        };

        ViewModel = new BadRequestObjectResult(problemDetails);
    }

    public void Default(DepositOutput depositOutput)
    {
        var depositResponse = new DepositResponse(
            depositOutput.Transaction.Amount,
            depositOutput.Transaction.Description,
            depositOutput.Transaction.TransactionDate,
            depositOutput.UpdatedBalance
        );
        ViewModel = new ObjectResult(depositResponse);
    }
}