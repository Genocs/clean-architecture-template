namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

public interface IUseCase
{
    Task ExecuteAsync(GetCustomerDetailsInput getCustomerDetailsInput);
}