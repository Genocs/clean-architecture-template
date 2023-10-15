namespace Genocs.CleanArchitecture.Template.Application.Services
{
    using Genocs.CleanArchitecture.Template.Shared.Interfaces;
    using System.Threading.Tasks;

    public interface IServiceBusClient
    {
        Task SendCommandAsync<T>(T cmd) where T : ICommand;
        Task PublishEventAsync<T>(T evt) where T : IEvent;
    }
}