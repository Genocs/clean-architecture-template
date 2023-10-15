namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public interface IUseCase
{
    Task Execute(CloseAccountInput closeAccountInput);
}