namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

public interface IUseCase
{
    Task ExecuteAsync(RefundInput refundInput);
}