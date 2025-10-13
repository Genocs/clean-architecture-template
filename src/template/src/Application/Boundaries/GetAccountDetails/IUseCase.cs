namespace Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;

public interface IUseCase
{
    Task ExecuteAsync(GetAccountDetailsInput getAccountDetailsInput);
}