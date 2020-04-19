namespace Genocs.WebApi.UseCases.V1.CloseAccount
{
    using Genocs.Application.Boundaries.CloseAccount;
    using Microsoft.AspNetCore.Mvc;

    public sealed class CloseAccountPresenter : IOutputPort
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

        public void Default(CloseAccountOutput closeAccountOutput)
        {
            ViewModel = new OkResult();
        }
    }
}