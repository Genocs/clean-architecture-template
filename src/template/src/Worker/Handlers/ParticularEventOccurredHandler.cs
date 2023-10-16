using Genocs.CleanArchitecture.Template.Shared.Events;
using Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;
using NServiceBus;
using NServiceBus.Logging;

namespace Genocs.CleanArchitecture.Template.Worker.Handlers;

/// <summary>
/// This event handler will be registered automatically. It do not need any action to be up and running.
/// </summary>
public class ParticularEventOccurredHandler : IHandleMessages<RegistrationCompleted>
{
    private readonly ILog _logger = LogManager.GetLogger<ParticularEventOccurredHandler>();

    // static int counter = 0;

    // public ParticularEventOccurredHandler(ILog logger)
    //    => _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

    public async Task Handle(RegistrationCompleted message, IMessageHandlerContext context)
    {

        _logger.Info($"RegistrationCompleted on CustomerId: '{message.CustomerId}', AccountId: '{message.AccountId}', ");

        // Start the saga
        await context.SendLocal(new TransactionLoaded()
        {
            RequestId = Guid.NewGuid().ToString(),
            TransactionId = Guid.NewGuid().ToString()
        });

        // Remove the comments to simulate some exception
        //if(counter++ < 10 )
        //{
        //    throw new InvalidOperationException($"exception number: '{counter}'");
        //}
    }
}
