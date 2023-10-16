using Microsoft.FeatureManagement;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;

public static class FeatureFlagsExtensions
{
    public static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration);

        var featureManager = services.BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();

        services.AddMvc()
            .ConfigureApplicationPartManager(apm =>
                apm.FeatureProviders.Add(
                    new CustomControllerFeatureProvider(featureManager)));

        return services;
    }
}
