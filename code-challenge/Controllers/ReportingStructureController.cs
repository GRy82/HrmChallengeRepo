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
    [Route("api/reporting-structure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _reportingStructureService = reportingStructureService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getReportStructureById")]
        public IActionResult GetReportStructureById(string id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var employeeWithReports = _reportingStructureService.GetReportingStructure(id);

            if (employeeWithReports == null)
                return NotFound();

            return Ok(employeeWithReports);
        }
    }
}
