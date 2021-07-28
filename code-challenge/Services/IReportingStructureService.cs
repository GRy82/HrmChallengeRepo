using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static challenge.Services.ReportingStructureService;

namespace challenge.Services
{
    public interface IReportingStructureService
    {
        Reportable GetReportingStructure();
    }
}
