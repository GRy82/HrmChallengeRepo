using challenge.Models;
using challenge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _empoyeeRepository;
        public ReportingStructureService(IEmployeeRepository employeeRepository)
        {
            _empoyeeRepository = employeeRepository;
        }
        public Employee GetReportingStructure(string id)
        {
            throw new NotImplementedException();
        }
    }
}
