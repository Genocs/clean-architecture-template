using Genocs.CleanArchitecture.Template.Application.Services;
using Newtonsoft.Json;

namespace Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.ExternalServices;


public abstract class ApiClient : IApiClient
{
    protected readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
        => _httpClient = httpClient;

    protected HttpContent PackageContent<T>(T transactionRequest) where T : class
    {
        StringContent content = new StringContent(JsonConvert.SerializeObject(transactionRequest));
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return content;
    }
}
