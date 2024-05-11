using Refit;

namespace Genocs.CleanArchitecture.Template.WebApi.ApiClient;

/// <summary>
/// This is an example of a Refit ApiClient interface.
/// </summary>
[Headers("Accept: application/json", "Authorization: Bearer xxxx")]
public interface IOrderApi
{
    /// <summary>
    /// Fool method to test the ApiClient.
    /// </summary>
    /// <param name="pageIndex">The page.</param>
    /// <param name="pageSize">The number of element inside the page.</param>
    /// <returns></returns>
    [Get("/orders")]
    Task<HttpResponseMessage> GetOrdersAsync([AliasAs("page")] int pageIndex, [AliasAs("size")] int pageSize);

}
