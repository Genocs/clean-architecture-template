namespace Genocs.MicroserviceLight.Template.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Genocs.MicroserviceLight.Template");

        [HttpGet("ping")]
        public IActionResult GetPing() => Ok();
    }
}