namespace Genocs.Application.Boundaries.Register
{
    public interface IOutputPort : IErrorHandler
    {
        void Standard(RegisterOutput registerOutput);
    }
}