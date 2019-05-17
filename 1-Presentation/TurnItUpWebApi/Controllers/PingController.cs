using Microsoft.AspNetCore.Mvc;
using TurnItUpWebApi.Filters;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/ping")]
    public class PingController : Controller
    {
        public PingController()
        {

        }

        [HttpGet]
        [Throttle(Name = "PingThrottle", Seconds = 5)]
        public IActionResult GetPing()
        {
            return new OkObjectResult("Ping");
        }

        [HttpGet]
        [Route("pong")]
        [Throttle(Name = "PongThrottle", Seconds = 5)]
        public IActionResult GetPong()
        {
            var x = this.HttpContext;
            return new OkObjectResult("Pong");
        }
    }
}
