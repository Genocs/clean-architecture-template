using Genocs.CleanArchitecture.Template.Contracts.Events;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Worker.MassTransitSB.Handlers;

public class MassTransitEventOccurredHandler : IConsumer<IntegrationEventIssued>
{
    private readonly ILogger<MassTransitEventOccurredHandler> _logger;

    public MassTransitEventOccurredHandler(ILogger<MassTransitEventOccurredHandler> logger)
        => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public Task Consume(ConsumeContext<IntegrationEventIssued> context)
    {
        _logger.LogDebug("Processed message");

        // Do something with the message here
        return Task.CompletedTask;
    }
}
