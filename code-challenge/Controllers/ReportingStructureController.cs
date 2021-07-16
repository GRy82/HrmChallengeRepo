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

            var employeeRepository = (EmployeeRepository)this.HttpContext.RequestServices.GetService(typeof(IEmployeeRepository));
            var employee = new ReportingStructure(id, employeeRepository);
            var employeeWithReports = employee.GetReportingStructure(); 

            if (employeeWithReports == null)
                return NotFound();

            var reportable = employee.FormatReportingStructure(employeeWithReports);
            return Ok(reportable);
        }
    }
}
