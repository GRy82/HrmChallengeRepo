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
        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
        }

        public Employee GetReportingStructure(string id)
        {
<<<<<<< HEAD
            return _employeeRepository.GetDirectReports(id);
            //var employee = _employeeRepository.GetById(id);
            //employee.DirectReports = StructureReports(employee);

            //return employee;
=======
            _employeeRepository.GetDirectReports(id);
            return new Employee();
>>>>>>> 7f79f51b7f3d37424ce24fafc49df9e2157f4fb2
        }

        //private List<Employee> StructureReports(Employee currentEmployee)
        //{
        //    var directReports = _employeeRepository.GetDirectReports(currentEmployee.EmployeeId);
        //    if(directReports != null)
        //    {
        //        foreach(var report in directReports)
        //        {
        //            report.DirectReports = StructureReports(report);
        //        }
        //    }

        //    return directReports;
        //}
    }
}
