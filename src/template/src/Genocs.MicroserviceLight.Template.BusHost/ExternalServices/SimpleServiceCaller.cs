using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.ExternalServices
{
    using Exceptions;
    using Models;

    public class SimpleServiceCaller : ISimpleServiceCaller
    {
        private readonly HttpClient _httpClient;

        public SimpleServiceCaller(HttpClient httpClient)
            => _httpClient = httpClient;

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
