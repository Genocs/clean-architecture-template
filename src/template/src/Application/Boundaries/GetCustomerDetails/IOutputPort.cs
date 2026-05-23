using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

public interface IOutputPort : IOutputPort<GetCustomerDetailsOutput>
{
    void NotFound(string message);
}