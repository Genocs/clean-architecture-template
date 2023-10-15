using Genocs.CleanArchitecture.Template.Application.Exceptions;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

public sealed class GetCustomerDetailsInput
{
    public Guid CustomerId { get; }

    public GetCustomerDetailsInput(Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new InputValidationException($"{nameof(customerId)} cannot be empty.");
        }

        CustomerId = customerId;
    }
}