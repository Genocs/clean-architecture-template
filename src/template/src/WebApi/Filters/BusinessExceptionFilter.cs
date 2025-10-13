using Genocs.CleanArchitecture.Template.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Genocs.CleanArchitecture.Template.WebApi.Filters;

public sealed class BusinessExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DomainException domainException)
        {
            var problemDetails = new ProblemDetails
            {
                Status = 400,
                Title = "Bad Request",
                Detail = domainException.Message
            };

            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}
