namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;

public interface IUseCase
{
    Task Execute(GetAccountDetailsInput getAccountDetailsInput);
}