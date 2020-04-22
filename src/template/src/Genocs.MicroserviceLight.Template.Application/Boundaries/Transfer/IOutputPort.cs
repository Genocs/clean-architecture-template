namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer
{
    public interface IOutputPort : IErrorHandler
    {
        void Default(TransferOutput transferOutput);
    }
}