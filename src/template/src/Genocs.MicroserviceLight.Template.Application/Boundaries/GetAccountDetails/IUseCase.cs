namespace Genocs.MicroserviceLight.Template.Application.Boundaries.GetAccountDetails
{
    using System.Threading.Tasks;

    public interface IUseCase
    {
        Task Execute(GetAccountDetailsInput getAccountDetailsInput);
    }
}