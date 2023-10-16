using Genocs.CleanArchitecture.Template.Shared.Interfaces;

namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;

public interface IMessageEventHandler<in TIntegrationEvent> : IMessageEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IMessageEventHandler
{

}
