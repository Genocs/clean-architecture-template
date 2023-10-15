namespace Genocs.MicroserviceLight.Template.WebApi.Extensions;

using Microsoft.Extensions.DependencyInjection;
using WebApi.Filters;

public static class BusinessExceptionExtensions
{
    public static IServiceCollection AddBusinessExceptionFilter(this IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(BusinessExceptionFilter));
        });

        return services;
    }
}
