using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.RebusSB;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class RebusServicebusInfrastructureExtensions
{
    public static IServiceCollection AddRebusServiceBus(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RebusBusSettings>(config.GetSection(RebusBusSettings.Position));

        services.AddSingleton<IServiceBusClient, RebusServiceBusClient>();

        return services;
    }
}
