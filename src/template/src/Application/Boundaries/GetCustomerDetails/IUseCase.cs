namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

public interface IUseCase
{
    Task Execute(GetCustomerDetailsInput getCustomerDetailsInput);
}