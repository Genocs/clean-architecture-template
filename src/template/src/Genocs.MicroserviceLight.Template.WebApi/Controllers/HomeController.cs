using Microsoft.AspNetCore.Mvc;

namespace Genocs.MicroserviceLight.Template.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index() { return Ok(); }

    }
}
