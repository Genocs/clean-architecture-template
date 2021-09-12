namespace Genocs.MicroserviceLight.Template.BusWorker.HostedServices
{
    using Infrastructure.ServiceBus;
    using Template.Shared.ReadModels;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;

    internal class AzureBusHostService:  AzureBusService
    {
        public AzureBusHostService(IOptions<AzureServiceBusSettings> options, 
                                    ILogger<AzureBusService> logger, 
                                    IServiceProvider serviceProvider): base(options, logger, serviceProvider)
        {
            RegisterMessage<IntegrationEventIssued, IMessageEventHandler<IntegrationEventIssued>>();
        }
    }
}
