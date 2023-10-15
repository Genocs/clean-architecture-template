namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws
{
    using System.Threading.Tasks;

    public interface IUseCase
    {
        Task Execute(WithdrawInput withdrawInput);
    }
}