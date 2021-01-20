namespace Genocs.MicroserviceLight.Template.Infrastructure.WebApiClient.ExternalServices
{
    using Application.Services;
    using BusHost.Exceptions;
    using Shared.ReadModels;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AuthApiClient : ApiClient, IAuthApiClient
    {
        public AuthApiClient(HttpClient httpClient) : base(httpClient) { }

        public async Task<SimpleResult> GetSimpleAuthModelAsync(string id)
        {
            try
            {
                var request = CreateChangeStatusSchedule(id);
                var content = PackageContent(request);
                var response = await _httpClient.PostAsync($"Authorized/{id}", content);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return await response.Content.ReadAsAsync<SimpleResult>();
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return await this.GetPackageAsync(id);
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

        private async Task<SimpleResult> GetPackageAsync(string messageId)
        {
            try
            {
                var request = CreateChangeStatusSchedule(messageId);
                var content = PackageContent(request);
                var response = await _httpClient.PutAsJsonAsync($"Authorized/{messageId}", content);

                if (response.StatusCode == HttpStatusCode.OK)
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

        private ChangeStatusSchedule CreateChangeStatusSchedule(string messageId)
        {
            // Fake data 
            ChangeStatusSchedule changeStatus = new ChangeStatusSchedule
            {
                MessageId = messageId,
                StatusId = "START",
                DateEvent = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            return changeStatus;
        }
    }
}
