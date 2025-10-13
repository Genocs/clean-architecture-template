using Genocs.CleanArchitecture.Template.Contracts.Events;

namespace Genocs.CleanArchitecture.Template.Worker.Handlers;

public class RebusEventOccurredHandler : Rebus.Handlers.IHandleMessages<RegistrationCompleted>
{
    private readonly ILogger _logger;

    public RebusEventOccurredHandler(ILogger logger)
        => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(RegistrationCompleted message)
    {
        _logger.LogInformation("Got string: {0}", message.CreditId);
        await Task.CompletedTask;
    }
}
