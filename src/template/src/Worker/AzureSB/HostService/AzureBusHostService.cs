using Genocs.CleanArchitecture.Template.Contracts.Events;
using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Worker.AzureSB.HostService;

internal class AzureBusHostService : AzureBusService
{
    public AzureBusHostService(
                                IOptions<AzureServiceBusSettings> options,
                                ILogger<AzureBusService> logger,
                                IServiceProvider serviceProvider)
        : base(options, logger, serviceProvider)
    {
        RegisterMessage<IntegrationEventIssued, IMessageEventHandler<IntegrationEventIssued>>();
    }
}
