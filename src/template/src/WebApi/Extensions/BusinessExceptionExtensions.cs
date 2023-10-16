using Genocs.CleanArchitecture.Template.WebApi.Filters;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

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
