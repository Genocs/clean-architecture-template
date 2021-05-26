namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus.Azure
{
    using Genocs.MicroserviceLight.Template.Shared.Interfaces;
    using System.Threading.Tasks;

    public interface IMessageEventHandler<in TIntegrationEvent> : IMessageEventHandler where TIntegrationEvent : IIntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IMessageEventHandler
    {

    }
}
