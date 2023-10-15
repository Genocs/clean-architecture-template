namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;

using System.Threading.Tasks;

public interface IUseCase
{
    Task Execute(GetCustomerDetailsInput getCustomerDetailsInput);
}