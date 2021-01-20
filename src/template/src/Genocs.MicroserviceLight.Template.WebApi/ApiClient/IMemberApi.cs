using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.WebApi.ApiClient
{
    [Headers("Accept: application/json", "Authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiIsImtpZCI6InN2YiJ9.eyJzdWIiOjEsImlhdCI6MTYxMTEzMDYxNCwiZXhwIjoxNjExMjE3MDE0LCJlbWFpbCI6ImFkbWluQHV0dS5nbG9iYWwiLCJnaXZlbl9uYW1lIjoiQWRtaW4iLCJmYW1pbHlfbmFtZSI6IkFkbWluIiwicHJlZmVycmVkX3VzZXJuYW1lIjoiYWRtaW4iLCJyb2xlcyI6WyJBZG1pbiIsIk1hbmFnZXIiLCJNZW1iZXIiXSwiZG9tYWluX2lkIjoiYWRtaW4ifQ.Vaj0JGJJrQwQLnSJY7U2qUSF6agGVHzYRx_TkgGLFxn4xN4ARDk-GH8eLow_6eeAprf9cdnkOX70uUEqaDs8HVCHDPA2-zeqBX4E5-72qtLWnlZA_qkxti-EMfEPxF52mt9DwmWdGI2mE3Oh0EgtFv6iP1h_538iTBiQAe6kk-k")]
    public interface IMemberApi
    {
        [Get("/memberquery")]
        Task<HttpResponseMessage> GetMemberAsync([AliasAs("attachImages")] bool attachImages, [AliasAs("memberId")] string memberId);
    }
}
