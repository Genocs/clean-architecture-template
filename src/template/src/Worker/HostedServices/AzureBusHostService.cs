namespace Genocs.CleanArchitecture.Template.Worker.HostedServices
{
    using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure;
    using Genocs.CleanArchitecture.Template.Shared.Events;
    using Infrastructure.ServiceBus;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Shared.ReadModels;
    using System;

    internal class AzureBusHostService : AzureBusService
    {
        public AzureBusHostService(IOptions<AzureServiceBusSettings> options,
                                    ILogger<AzureBusService> logger,
                                    IServiceProvider serviceProvider) : base(options, logger, serviceProvider)
        {
            RegisterMessage<IntegrationEventIssued, IMessageEventHandler<IntegrationEventIssued>>();
        }
    }
}
