using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;

public interface IOutputPort : IOutputPort<GetAccountDetailsOutput>
{
    void NotFound(string message);
}