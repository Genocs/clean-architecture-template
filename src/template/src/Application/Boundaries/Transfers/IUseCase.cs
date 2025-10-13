namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;

public interface IUseCase
{
    Task ExecuteAsync(TransferInput transferInput);
}