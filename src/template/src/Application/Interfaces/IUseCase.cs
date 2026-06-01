namespace Genocs.CleanArchitecture.Template.Application.Interfaces;

/// <summary>
/// Generic interface for use cases in the application layer. It defines a contract for executing a use case with a specific input type.
/// </summary>
/// <typeparam name="TInput">The type of input required by the use case.</typeparam>
public interface IUseCase<TInput>
    where TInput : IInputType
{
    Task ExecuteAsync(TInput input, CancellationToken cancellationToken = default);
}