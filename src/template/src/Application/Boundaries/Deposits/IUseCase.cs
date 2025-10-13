namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;

public interface IUseCase
{
    Task ExecuteAsync(DepositInput depositInput);
}