//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace TurnItUpWebApi.Controllers
//{
//	[Route("v1/ping")]
//	public class PingController : Controller
//	{
//		public PingController()
//		{

//		}

//		[HttpGet]
//		public IActionResult GetPing()
//		{
//			return new OkObjectResult("Ping");
//		}

//		[HttpGet]
//		[Route("pong")]
//		public IActionResult GetPong()
//		{
//			var x = this.HttpContext;
//			return new OkObjectResult("Pong");
//		}
//	}
//}
