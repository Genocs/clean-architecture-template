namespace Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetAccountDetails
{
    using Application.Boundaries.GetAccountDetails;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using WebApi.ViewModels;

    public sealed class GetAccountDetailsPresenter : IOutputPort
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

        public void Default(GetAccountDetailsOutput getAccountDetailsOutput)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var item in getAccountDetailsOutput.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);

                transactions.Add(transaction);
            }

            var getAccountDetailsResponse = new GetAccountDetailsResponse(
                getAccountDetailsOutput.AccountId,
                getAccountDetailsOutput.CurrentBalance,
                transactions);

            ViewModel = new OkObjectResult(getAccountDetailsResponse);
        }

        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }
    }
}