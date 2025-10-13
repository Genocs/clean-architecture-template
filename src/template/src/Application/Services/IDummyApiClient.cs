using Genocs.CleanArchitecture.Template.Contracts.ReadModels;

namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IDummyApiClient : IApiClient
{
    Task<SimpleResult> GetSimpleModelAsync(string id);
}
