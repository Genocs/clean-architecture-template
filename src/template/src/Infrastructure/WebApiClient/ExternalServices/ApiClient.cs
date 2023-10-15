namespace Genocs.MicroserviceLight.Template.Infrastructure.WebApiClient.ExternalServices
{
    using Application.Services;
    using Newtonsoft.Json;
    using System.Net.Http;

    public abstract class ApiClient : IApiClient
    {
        protected readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
            => _httpClient = httpClient;

        protected HttpContent PackageContent<T>(T transactionRequest) where T : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(transactionRequest));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
