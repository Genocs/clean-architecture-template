using Genocs.CleanArchitecture.Template.Contracts.Events;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Worker.MassTransitSB.Handlers;

public class DemoEventOccurredHandler(ILogger<DemoEventOccurredHandler> logger) : IConsumer<DemoEventOccurred>
{
    private readonly ILogger<DemoEventOccurredHandler> _logger = logger;

    public Task Consume(ConsumeContext<DemoEventOccurred> context)
    {
        _logger.LogInformation(context.Message.Payload);
        return Task.CompletedTask;
    }
}
