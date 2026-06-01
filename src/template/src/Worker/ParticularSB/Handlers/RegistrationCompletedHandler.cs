using NServiceBus.Logging;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB.Handlers;

public class RegistrationCompletedHandler : IHandleMessages<ContractsNServiceBus.Events.ParticularRegistrationCompleted>
{
    private readonly ILog _logger = LogManager.GetLogger<RegistrationCompletedHandler>();

    public async Task Handle(ContractsNServiceBus.Events.ParticularRegistrationCompleted message, IMessageHandlerContext context)
    {
        _logger.Info($"RegistrationCompleted successfully. CreditId: {message.CreditId}");

        // Start the saga
        // await context.Publish(TransactionLoaded.Fake());

        // Remove the comments to simulate some exception
        // if(counter++ < 10 )
        // {
        //     throw new InvalidOperationException($"exception number: '{counter}'");
        // }
    }
}
