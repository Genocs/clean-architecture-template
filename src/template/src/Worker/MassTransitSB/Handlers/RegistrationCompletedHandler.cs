using Genocs.CleanArchitecture.Template.Contracts.Events;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Worker.MassTransitSB.Handlers;

public class RegistrationCompletedHandler(ILogger<RegistrationCompletedHandler> logger) : IConsumer<RegistrationCompleted>
{
    private readonly ILogger<RegistrationCompletedHandler> _logger = logger;

    public Task Consume(ConsumeContext<RegistrationCompleted> context)
    {
        _logger.LogInformation(context.Message.AccountId.ToString());
        return Task.CompletedTask;
    }
}