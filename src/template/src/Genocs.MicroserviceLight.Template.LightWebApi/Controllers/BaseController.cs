namespace Genocs.MicroserviceLight.Template.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Dispatchers;
    using Genocs.Microservices.Common.Types;

    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _dispatcher.QueryAsync<TResult>(query);

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        protected ActionResult<PagedResult<T>> Collection<T>(PagedResult<T> pagedResult)
        {
            if (pagedResult == null)
            {
                return NotFound();
            }

            return Ok(pagedResult);
        }
    }
}