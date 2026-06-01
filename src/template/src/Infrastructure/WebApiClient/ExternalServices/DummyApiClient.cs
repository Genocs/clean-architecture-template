using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.Exceptions;
using Genocs.CleanArchitecture.Template.Contracts.ReadModels;

namespace Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.ExternalServices;

public class DummyApiClient : ApiClient, IDummyApiClient
{
    public DummyApiClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<SimpleResult> GetSimpleModelAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Dummy/{id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SimpleResult>(cancellationToken);
            }

            throw new BackendServiceCallFailedException(response.ReasonPhrase);
        }
        catch (BackendServiceCallFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new BackendServiceCallFailedException(e.Message, e);
        }
    }
}
