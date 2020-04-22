namespace Genocs.MicroserviceLight.Template.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Dispatchers;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Dto;
    using Genocs.MicroserviceLight.Template.Queries;

    public class FooTemplatesController : BaseController
    {

        public FooTemplatesController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("")]
        public async Task<ActionResult<PagedResult<FooTemplateDto>>> Get([FromQuery] BrowseFooTemplates query)
            => Collection(await QueryAsync(query));

        [HttpGet("FooTemplate/{id:guid}")]
        public async Task<ActionResult<FooTemplateDto>> Get([FromRoute] GetFooTemplatesDto query)
            => Single(await QueryAsync(query));
    }
}