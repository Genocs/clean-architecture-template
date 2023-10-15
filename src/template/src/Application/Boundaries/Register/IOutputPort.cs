namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Register
{
    public interface IOutputPort : IErrorHandler
    {
        void Standard(RegisterOutput registerOutput);
    }
}