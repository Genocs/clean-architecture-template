using Genocs.CleanArchitecture.Template.Contracts.ReadModels;

namespace Genocs.CleanArchitecture.Template.Application.Services;

public interface IAuthApiClient : IApiClient
{
    Task<SimpleResult> GetSimpleAuthModelAsync(string id);
}
