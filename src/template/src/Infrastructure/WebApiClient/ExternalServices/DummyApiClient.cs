namespace Genocs.MicroserviceLight.Template.Infrastructure.WebApiClient.ExternalServices
{
    using Application.Services;
    using BusWorker.Exceptions;
    using Shared.ReadModels;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class DummyApiClient : ApiClient, IDummyApiClient
    {
        public DummyApiClient(HttpClient httpClient) : base(httpClient) { }

        public async Task<SimpleResult> GetSimpleModelAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Dummy/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<SimpleResult>();
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
}
