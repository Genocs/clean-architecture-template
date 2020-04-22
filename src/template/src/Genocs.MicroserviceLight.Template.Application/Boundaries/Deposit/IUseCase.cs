namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Deposit
{
    using System.Threading.Tasks;

    public interface IUseCase
    {
        Task Execute(DepositInput depositInput);
    }
}