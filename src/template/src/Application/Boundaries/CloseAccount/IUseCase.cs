namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public interface IUseCase
{
    Task ExecuteAsync(CloseAccountInput closeAccountInput);
}