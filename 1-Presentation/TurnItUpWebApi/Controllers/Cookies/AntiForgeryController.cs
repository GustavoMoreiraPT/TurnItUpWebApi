using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers.Cookies
{
    [Route("v1/antiforgery")]
    public class AntiForgeryController : Controller
    {
        [HttpGet]
        //[GenerateAntiforgeryTokenCookieForAjax]
        public IActionResult Index()
        {
            return View();
        }
    }
}
