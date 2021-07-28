using challenge.Models;
using challenge.Repositories;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/reporting-structure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        
        public ReportingStructureController(ILogger<ReportingStructureController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getReportStructureById")]
        public IActionResult GetReportStructureById(string id, [FromServices] IEmployeeRepository employeeRepository)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            bool employeeExists = employeeRepository.GetById(id) != null;
            if (!employeeExists)
                return NotFound();

            IReportingStructureService employeeStructure = new ReportingStructureService(id, employeeRepository);
            var reportableStructure = employeeStructure.GetReportingStructure(); 

            return Ok(reportableStructure);
        }
    }
}
