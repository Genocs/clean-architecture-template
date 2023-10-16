using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System.Collections;
using System.Reflection;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;

public sealed class CustomControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private readonly IFeatureManager _featureManager;

    public CustomControllerFeatureProvider(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        for (int i = feature.Controllers.Count - 1; i >= 0; i--)
        {
            var controller = feature.Controllers[i].AsType();
            foreach (var customAttribute in controller.CustomAttributes)
            {
                if (customAttribute.AttributeType.FullName == typeof(FeatureGateAttribute).FullName)
                {
                    var constructorArgument = customAttribute.ConstructorArguments.First();
                    foreach (object? argumentValue in constructorArgument.Value as IEnumerable)
                    {
                        var typedArgument = (CustomAttributeTypedArgument)argumentValue;
                        var typedArgumentValue = (Features)(int)typedArgument.Value;
                        if (!await _featureManager.IsEnabledAsync(typedArgumentValue.ToString()))
                            feature.Controllers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
