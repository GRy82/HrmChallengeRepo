using challenge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class ReportingStructure
    {
        public int numberOfReports;

        public string employeeId;

        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructure(string employeeId, IEmployeeRepository employeeRepository)
        {
            this.employeeId = employeeId;
            _employeeRepository = employeeRepository;
        }

        public Employee GetReportingStructure()
        {
            return _employeeRepository.GetDirectReports(this.employeeId);

        }

        private List<Employee> StructureReports(string id)
        {
            var directReports = _employeeRepository.GetDirectReports(id);
            if (directReports != null)
            {
                foreach (var report in directReports)
                {
                    report.DirectReports = StructureReports(report);
                }
            }

            return directReports;
        }
    }
}
