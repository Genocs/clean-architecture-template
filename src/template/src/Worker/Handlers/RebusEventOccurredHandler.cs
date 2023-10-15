namespace Genocs.CleanArchitecture.Template.Worker.Handlers
{
    using Genocs.CleanArchitecture.Template.Shared.Events;
    using Microsoft.Extensions.Logging;
    using Rebus.Handlers;
    using Shared.Events;
    using System.Threading.Tasks;

    class RebusEventOccurredHandler : IHandleMessages<RegistrationCompleted>
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
}
