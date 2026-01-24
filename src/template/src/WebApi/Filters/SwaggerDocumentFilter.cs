using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Genocs.CleanArchitecture.Template.WebApi.Filters;

public class SwaggerDocumentFilter : IDocumentFilter
{
    private readonly List<OpenApiTag> _tags = new List<OpenApiTag>
        {
            new OpenApiTag
            {
                Name = "RoutingApi",
                Description = "This is a description for the api routes"
            }
        };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(swaggerDoc);

        swaggerDoc.Tags = GetFilteredTagDefinitions(context);
        swaggerDoc.Paths = GetSortedPaths(swaggerDoc);
    }

    private ISet<OpenApiTag> GetFilteredTagDefinitions(DocumentFilterContext context)
    {
        // Filtering ensures route for tag is present
        var currentGroupNames = context.ApiDescriptions
            .Select(description => description.GroupName);
        return _tags.Where(tag => currentGroupNames.Contains(tag.Name))
            .ToHashSet();
    }

    private static OpenApiPaths GetSortedPaths(OpenApiDocument swaggerDoc)
    {
        IDictionary<string, IOpenApiPathItem> dic = swaggerDoc.Paths.OrderBy(pair => pair.Key)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        return [];
    }
}
