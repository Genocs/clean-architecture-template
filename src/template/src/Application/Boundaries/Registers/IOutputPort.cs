namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
public interface IOutputPort : IErrorHandler
{
    void Standard(RegisterOutput registerOutput);
}