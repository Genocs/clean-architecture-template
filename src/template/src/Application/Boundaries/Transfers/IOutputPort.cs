namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;

public interface IOutputPort : IErrorHandler
{
    void Default(TransferOutput transferOutput);
}