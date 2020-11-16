using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("/api/yoyo")]
    public class YoYoController : Controller
    {
        public ActionResult Get()
        {
            return Ok(new { Hello = "World!" });
        }
    }
}
