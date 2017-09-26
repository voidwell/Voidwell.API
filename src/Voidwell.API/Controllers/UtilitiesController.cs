using Microsoft.AspNetCore.Mvc;
using System;

namespace Voidwell.API.Controllers
{
    [Route("/")]
    public class UtilitiesController : Controller
    {
        [HttpGet("time")]
        public ActionResult GetTimeUTC()
        {
            var dateObj = new
            {
                dateString = DateTime.UtcNow
            };

            return Ok(dateObj);
        }
    }
}
