using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using challenge.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        public int numberOfReports;

        public string employeeId;

        private readonly IEmployeeRepository _employeeRepository;

        public ReportingStructureService(string employeeId, IEmployeeRepository employeeRepository)
        {
            this.numberOfReports = 0;
            this.employeeId = employeeId;
            _employeeRepository = employeeRepository;
        }

        // This class provides a restructuring for the json response where the full reporting structure can be reported in conjunction with
        // employeeId at the top of the structure, and their numberOfReports.
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


        // Instead of creating and registering a separate service to perform the functionality below, since ReportingStructure objects
        // are not persisted in the database, there were advantages to having this be an instance method. 1.) employeeId can be passed
        // into the constructor at instantiation. 2.) The numberOfReports field is tracked with ease and can be incremented in one line
        // of code. 3.) Operations for this class may as well be self-contained/encapsulated together as they're not needed elsewhere.

        // This method initiates the recursive process.
        public Reportable GetReportingStructure()
        {
            ResetNumberOfReports();
            Employee structuredReports = StructureReports(employeeId);
            return new Reportable(employeeId, numberOfReports, structuredReports);
        }

        // This recursive function collects employee ojects that are organized within a report structure in a breadth-first manner, and
        // simultaneously tracks each addition to the structure by updating the numberOfReports field.
        private Employee StructureReports(string id)
        {
            var employee = _employeeRepository.GetDirectReports(id);

            if (employee.DirectReports == null)
                return null;

            for (int i = 0; i < employee.DirectReports.Count; i++)
            {
                numberOfReports++;
                employee.DirectReports[i] = StructureReports(employee.DirectReports[i].EmployeeId);
            }

            return employee;
        }

        private void ResetNumberOfReports()
        {
            numberOfReports = 0;
        }
    }
}
