namespace Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;

public interface IOutputPort : IErrorHandler
{
    void Default(CloseAccountOutput closeAccountOutput);
}