namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure
{
    using Genocs.CleanArchitecture.Template.Shared.Interfaces;
    using System.Threading.Tasks;

    public interface IMessageEventHandler<in TIntegrationEvent> : IMessageEventHandler where TIntegrationEvent : IIntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IMessageEventHandler
    {

    }
}
