using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
using Genocs.CleanArchitecture.Template.Shared.Events;
using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

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
