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
        public IActionResult GetReportStructureById(string id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            // IEmployeeRepository used once per instantiation of a ReportingStructure instance
            var employeeRepository = (EmployeeRepository)this.HttpContext.RequestServices.GetService(typeof(IEmployeeRepository));
            IReportingStructureService employeeStructure = new ReportingStructureService(id, employeeRepository);
            var reportableStructure = employeeStructure.GetReportingStructure(); 

            if (reportableStructure == null)
                return NotFound();

            return Ok(reportableStructure);
        }
    }
}
