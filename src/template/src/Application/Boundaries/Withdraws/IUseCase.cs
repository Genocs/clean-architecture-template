namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;

public interface IUseCase
{
    Task Execute(WithdrawInput withdrawInput);
}