using Genocs.CleanArchitecture.Template.Contracts.Events;
using Genocs.CleanArchitecture.Template.ContractsNServiceBus.TransactionSaga;
using NServiceBus.Logging;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB.Handlers;

public class RegistrationCompletedHandler : IHandleMessages<ContractsNServiceBus.Events.RegistrationCompleted>
{
    private readonly ILog _logger = LogManager.GetLogger<RegistrationCompletedHandler>();

    public async Task Handle(ContractsNServiceBus.Events.RegistrationCompleted message, IMessageHandlerContext context)
    {
        _logger.Info($"RegistrationCompleted successfully. CreditId: {message.CreditId}");

        // Start the saga
        /*
        await context.SendLocal(new TransactionLoaded()
        {
            RequestId = Guid.NewGuid().ToString(),
            TransactionId = Guid.NewGuid().ToString()
        });
        */

        // Remove the comments to simulate some exception
        //if(counter++ < 10 )
        //{
        //    throw new InvalidOperationException($"exception number: '{counter}'");
        //}
    }
}
