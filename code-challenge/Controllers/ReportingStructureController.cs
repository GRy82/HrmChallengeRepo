<<<<<<< HEAD
﻿using challenge.Models;
using challenge.Services;
=======
﻿using challenge.Services;
>>>>>>> 7f79f51b7f3d37424ce24fafc49df9e2157f4fb2
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
