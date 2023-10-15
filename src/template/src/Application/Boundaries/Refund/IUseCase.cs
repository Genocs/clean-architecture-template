namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Refund
{
    using System.Threading.Tasks;

    public interface IUseCase
    {
        Task Execute(RefundInput refundInput);
    }
}