using Asp.Versioning.ApiExplorer;
using Genocs.CleanArchitecture.Template.WebApi.Filters;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            // Add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();

            // Integrate xml comments
            string documentationFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            options.IncludeXmlComments(documentationFile);
        });

        return services;
    }

    public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, Action<SwaggerOptions>? setupAction = null)
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }

            /*
            if (_provider != null && _provider.ApiVersionDescriptions != null)
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            }
            else
            {
                // Default behavior
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Genocs.CleanArchitecture.Template API V1");
            }
            */

            options.InjectStylesheet("/swagger-ui/custom.css");

            // options.RoutePrefix = string.Empty;
        });

        return app;
    }
}
