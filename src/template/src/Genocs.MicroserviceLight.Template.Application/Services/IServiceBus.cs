namespace Genocs.MicroserviceLight.Template.Application.Services
{
    using System.Threading.Tasks;

    public interface IServiceBus
    {
        Task SendCommandAsync<T>(T cmd) where T : Shared.Interfaces.ICommand;
        Task PublishEventAsync<T>(T evt) where T : Shared.Interfaces.IEvent;
    }
}