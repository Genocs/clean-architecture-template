namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;

public interface IUseCase
{
    Task ExecuteAsync(WithdrawInput withdrawInput);
}