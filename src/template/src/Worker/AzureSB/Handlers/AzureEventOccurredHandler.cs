using Genocs.CleanArchitecture.Template.Contracts.Events;
using Genocs.CleanArchitecture.Template.Infrastructure.AzureSB;

namespace Genocs.CleanArchitecture.Template.Worker.AzureSB.Handlers;

public class AzureEventOccurredHandler : IMessageEventHandler<IntegrationEventIssued>
{
    private readonly ILogger<AzureEventOccurredHandler> _logger;

    public AzureEventOccurredHandler(ILogger<AzureEventOccurredHandler> logger)
        => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public Task Handle(IntegrationEventIssued @event)
    {
        _logger.LogDebug("Processed message");

        // Do something with the message here
        return Task.CompletedTask;
    }
}
