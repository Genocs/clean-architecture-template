using Genocs.Common.CQRS.Commands;
using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IServiceBusClient
{
    Task SendCommandAsync<T>(T cmd)
        where T : ICommand;

    Task PublishEventAsync<T>(T evt)
        where T : IEvent;
}