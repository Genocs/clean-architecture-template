using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.ExternalServices;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.Resiliency;

namespace Genocs.CleanArchitecture.Template.Worker.WebApi;

public static class WebApiServicesExtensions
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register API client
        services
            .AddHttpClient<IDummyApiClient, DummyApiClient>(c =>
            {
                c.BaseAddress = new Uri(configuration["ExternalWebServices:Order"]!);
            })
            .AddResiliencyPolicies(configuration);

        // Register Auth API client
        services
            .AddHttpClient<IAuthApiClient, AuthApiClient>(c =>
            {
                c.BaseAddress = new Uri(configuration["ExternalWebServices:Basket"]!);
                c.DefaultRequestHeaders.Add("Authorization", "Bearer your-token");
            })
            .AddResiliencyPolicies(configuration);

        return services;
    }
}
