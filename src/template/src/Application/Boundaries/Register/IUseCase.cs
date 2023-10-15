namespace Genocs.MicroserviceLight.Template.Application.Boundaries.Register
{
    using System.Threading.Tasks;

    public interface IUseCase
    {
        Task Execute(RegisterInput registerInput);
    }
}