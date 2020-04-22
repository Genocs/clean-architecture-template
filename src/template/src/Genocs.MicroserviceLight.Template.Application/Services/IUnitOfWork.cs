namespace Genocs.MicroserviceLight.Template.Application.Services
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}