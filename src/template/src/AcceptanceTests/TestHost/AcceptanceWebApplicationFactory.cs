using Genocs.CleanArchitecture.Template.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Genocs.CleanArchitecture.Template.AcceptanceTests.TestHost;

public sealed class AcceptanceWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IServiceBusClient, NoOpServiceBusClient>();
        });
    }

    private sealed class NoOpServiceBusClient : IServiceBusClient
    {
        public Task SendCommandAsync<T>(T command, CancellationToken cancellationToken = default)
            where T : Genocs.Common.CQRS.Commands.ICommand
        {
            return Task.CompletedTask;
        }

        public Task PublishEventAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : Genocs.Common.CQRS.Events.IEvent
        {
            return Task.CompletedTask;
        }
    }
}
