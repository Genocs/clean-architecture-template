using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Rebus;
using Genocs.CleanArchitecture.Template.Shared.Events;
using Genocs.CleanArchitecture.Template.Worker.Handlers;
using Microsoft.Extensions.Options;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

internal class RebusService : IHostedService
{
    private readonly ILogger<RebusService> _logger;
    private readonly RebusBusSettings _settings;

    private BuiltinHandlerActivator? _activator;
    private IBus? _bus;

    public RebusService(IOptions<RebusBusSettings> settings, ILogger<RebusService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _settings = settings.Value;

        if (_settings == null)
        {
            throw new NullReferenceException("options cannot be null");
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");

        // Start rebus configuration
        _activator = new BuiltinHandlerActivator();

        _activator.Register((r) => new RebusEventOccurredHandler(_logger));

        _bus = Configure.With(_activator)
            .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Debug))
            .Transport(t => t.UseRabbitMq(_settings.TransportConnection, _settings.QueueName))
            .Options(o => o.SetMaxParallelism(1))
            .Start();

        // Subscribe the event
        await _activator.Bus.Subscribe<RegistrationCompleted>();

        _logger.LogInformation("Started");

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        _logger.LogInformation("Stopped");
        await Task.CompletedTask;
    }
}
