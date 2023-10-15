namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Refund
{
    public interface IOutputPort : IErrorHandler
    {
        void Default(RefundOutput refundOutput);
    }
}