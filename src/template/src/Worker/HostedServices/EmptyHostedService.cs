using Genocs.CleanArchitecture.Template.Infrastructure.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

/// <summary>
/// Empty Hosted Service.
/// </summary>
internal class EmptyHostedService : IHostedService
{
    private readonly ILogger<EmptyHostedService> _logger;

    private readonly NullOptions _options;

    public EmptyHostedService(IOptions<NullOptions> options, ILogger<EmptyHostedService> logger)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
