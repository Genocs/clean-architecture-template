using Genocs.CleanArchitecture.Template.Contracts.Events;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.WorkerMassTransit.Handlers;

public class DemoEventOccurredHandler : IConsumer<DemoEventOccurred>
{
    private readonly ILogger<DemoEventOccurredHandler> _logger;

    public DemoEventOccurredHandler(ILogger<DemoEventOccurredHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<DemoEventOccurred> context)
    {
        _logger.LogInformation(context.Message.Payload);
        return Task.CompletedTask;
    }
}
