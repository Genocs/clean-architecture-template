namespace Genocs.CleanArchitecture.Template.Application.Interfaces;

public interface IOutputPort<TOutput> : IErrorHandler
    where TOutput : IOutputType
{
    void Default(TOutput output);
}