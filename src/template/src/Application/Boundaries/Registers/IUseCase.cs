namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

public interface IUseCase
{
    Task ExecuteAsync(RegisterInput registerInput);
}