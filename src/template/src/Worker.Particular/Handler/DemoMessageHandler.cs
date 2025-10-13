using Genocs.CleanArchitecture.Template.Worker.Particular.Messages;
using Genocs.CleanArchitecture.Template.Worker.Particular.Services;
using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Handler;

public class DemoMessageHandler(ICalculateStuff stuffCalculator) : IHandleMessages<DemoMessage>
{
    private readonly ICalculateStuff _stuffCalculator = stuffCalculator ?? throw new ArgumentNullException(nameof(stuffCalculator));

    public async Task Handle(DemoMessage message, IMessageHandlerContext context)
    {
        await _stuffCalculator.Calculate(message.Value);

        // Do some more stuff if needed
    }
}
