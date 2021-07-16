using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ICompensationService _compensationService;
        private readonly ILogger _logger;
       
        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _compensationService = compensationService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(string id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");
            var compensation = _compensationService.GetById(id);

            if(compensation != null)
                return Ok(compensation);

            return NotFound();
        }


        // Has an instance of IEmployeeService injected from IServiceCollection to this method only, where it is uniquely needed within this controller.
        // Use of IEmployeeService instance is needed to first confirm an employee with the given EmployeeId exists.
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation, [FromServices] IEmployeeService employeeService)
        {
            _logger.LogDebug($"Received compensation create request for employee with id: '{compensation.EmployeeId}' " +
                $"with salary of {compensation.Salary} and effective date of {compensation.EffectiveDate}");

            var employee = employeeService.GetById(compensation.EmployeeId);

            if (employee == null)
                return BadRequest();

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation);
        }
    }
}
