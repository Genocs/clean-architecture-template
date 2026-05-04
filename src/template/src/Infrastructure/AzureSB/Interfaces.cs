using Genocs.Common.CQRS.Events;

namespace Genocs.CleanArchitecture.Template.Infrastructure.AzureSB;

public interface IMessageEventHandler<in TIntegrationEvent> : IMessageEventHandler
    where TIntegrationEvent : IIntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

public interface IMessageEventHandler;
