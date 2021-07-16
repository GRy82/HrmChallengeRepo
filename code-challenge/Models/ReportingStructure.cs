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
            this.numberOfReports = 0;
            this.employeeId = employeeId;
            _employeeRepository = employeeRepository;
        }

        public class Reportable
        {
            public string employeeId { get; set; }
            public int numberOfReports { get; set; }
            public Employee employee { get; set; }

            public Reportable(string id, int reportNum, Employee employeeWithStructure)
            {
                employeeId = id;
                numberOfReports = reportNum;
                employee = employeeWithStructure;
            }
        }

        public Employee GetReportingStructure()
        {
            return StructureReports(employeeId);
        }

        private Employee StructureReports(string id)
        {
            var employee = _employeeRepository.GetDirectReports(id);

            if (employee.DirectReports == null)
                return null;

            for(int i = 0; i < employee.DirectReports.Count; i++)
            {
                numberOfReports++;
                employee.DirectReports[i] = StructureReports(employee.DirectReports[i].EmployeeId);
            }

            return employee;
        }

        public Reportable FormatReportingStructure(Employee reportingStructure)
        {
            return new Reportable(employeeId, numberOfReports, reportingStructure);
        }
    }
}
