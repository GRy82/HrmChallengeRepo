using challenge.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {   
        [HttpGet("{id}")]
        public IActionResult GetCompensationById()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            return Ok();
        }
    }
}
