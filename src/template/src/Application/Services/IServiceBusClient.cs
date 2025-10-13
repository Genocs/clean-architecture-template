using Genocs.CleanArchitecture.Template.Contracts.Interfaces;

namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IServiceBusClient
{
    Task SendCommandAsync<T>(T cmd)
        where T : ICommand;

    Task PublishEventAsync<T>(T evt)
        where T : IEvent;
}