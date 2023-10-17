using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Withdraw;

public sealed class WithdrawPresenter : IOutputPort
{
    public IActionResult? ViewModel { get; private set; }

    public void Error(string message)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = "An error occurred",
            Detail = message
        };

        ViewModel = new BadRequestObjectResult(problemDetails);
    }

    public void Default(WithdrawOutput withdrawOutput)
    {
        var withdrawResponse = new WithdrawResponse(
            withdrawOutput.Transaction.Amount,
            withdrawOutput.Transaction.Description,
            withdrawOutput.Transaction.TransactionDate,
            withdrawOutput.UpdatedBalance
        );
        ViewModel = new ObjectResult(withdrawResponse);
    }
}