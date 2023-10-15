namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public interface IOutputPort : IErrorHandler
{
    void Default(RefundOutput refundOutput);
}