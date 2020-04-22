namespace Genocs.MicroserviceLight.Template.Application.Boundaries.GetCustomerDetails
{
    using Genocs.MicroserviceLight.Template.Application.Exceptions;
    using System;

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
}