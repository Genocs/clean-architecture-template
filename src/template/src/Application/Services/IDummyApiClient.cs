using Genocs.CleanArchitecture.Template.Shared.ReadModels;

namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IDummyApiClient : IApiClient
{
    Task<SimpleResult> GetSimpleModelAsync(string id);
}
