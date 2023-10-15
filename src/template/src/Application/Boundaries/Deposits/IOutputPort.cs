namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;

public interface IOutputPort : IErrorHandler
{
    void Default(DepositOutput depositOutput);
}