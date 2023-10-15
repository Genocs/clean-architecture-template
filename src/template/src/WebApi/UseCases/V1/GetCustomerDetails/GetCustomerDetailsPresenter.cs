using Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;
using Genocs.CleanArchitecture.Template.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetCustomerDetails;

public sealed class GetCustomerDetailsPresenter : IOutputPort
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

    public void Default(GetCustomerDetailsOutput getCustomerDetailsOutput)
    {
        List<AccountDetailsModel> accounts = new List<AccountDetailsModel>();

        foreach (var account in getCustomerDetailsOutput.Accounts)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var item in account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);

                transactions.Add(transaction);
            }

            accounts.Add(new AccountDetailsModel(
                account.AccountId,
                account.CurrentBalance,
                transactions));
        }

        var getCustomerDetailsResponse = new GetCustomerDetailsResponse(
            getCustomerDetailsOutput.CustomerId,
            getCustomerDetailsOutput.SSN,
            getCustomerDetailsOutput.Name,
            accounts
        );

        ViewModel = new OkObjectResult(getCustomerDetailsResponse);
    }

    public void NotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }
}