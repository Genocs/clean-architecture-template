namespace Genocs.MicroserviceLight.Template.BusWorker.HostServices
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Empty Hosted Service
    /// </summary>
    internal class EmptyHostedService : IHostedService
    {
        private readonly ILogger<EmptyHostedService> _logger;

        private readonly Infrastructure.Generic.NullOptions _options;

        public EmptyHostedService(IOptions<Infrastructure.Generic.NullOptions> options, ILogger<EmptyHostedService> logger)
        {
            if (options is null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }

            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");
            _logger.LogInformation("Started");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            _logger.LogInformation("Stopped");
            await Task.CompletedTask;
        }
    }
}
