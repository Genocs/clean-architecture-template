using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Transfer;

public sealed class TransferPresenter : IOutputPort
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

    public void Default(TransferOutput transferOutput)
    {
        var transferResponse = new
        {
            transferOutput.Transaction.Amount,
            transferOutput.Transaction.Description,
            transferOutput.Transaction.TransactionDate,
            transferOutput.UpdatedBalance,
        };

        ViewModel = new ObjectResult(transferResponse);
    }
}