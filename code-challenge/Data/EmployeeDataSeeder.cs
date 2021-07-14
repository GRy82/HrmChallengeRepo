using challenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeDataSeeder
    {
        private ApplicationContext _applicationContext;
        private const String EMPLOYEE_SEED_DATA_FILE = "resources/EmployeeSeedData.json";

        public EmployeeDataSeeder(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Seed()
        {
            if(!_applicationContext.Employees.Any())
            {
                List<Employee> employees = LoadEmployees();
                _applicationContext.Employees.AddRange(employees);

                await _applicationContext.SaveChangesAsync();
            }
        }

        private List<Employee> LoadEmployees()
        {
            using (FileStream fs = new FileStream(EMPLOYEE_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                List<Employee> employees = serializer.Deserialize<List<Employee>>(jr);
                FixUpReferences(employees);

                return employees;
            }
        }

        private void FixUpReferences(List<Employee> employees)
        {
            //references to all employees
            var employeeIdRefMap = from employee in employees
                                   select new { Id = employee.EmployeeId, EmployeeRef = employee };

            //foreach employee
            employees.ForEach(employee =>
            {
                //if there are extant direct reports
                if (employee.DirectReports != null)
                {
                    //create list to hold those direct reports, "referencedEmployees"
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                    //foreach direct report
                    employee.DirectReports.ForEach(report =>
                    {
                        // retrieve reference to the employee from the entire list
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == report.EmployeeId).EmployeeRef;
                        referencedEmployees.Add(referencedEmployee);
                    });
                    employee.DirectReports = referencedEmployees;
                }
            });
        }



        //private void fixupreferences(employee[] employees)
        //{
        //    var employeeidrefmap = from employee in employees
        //                           select new { id = employee.employeeid, employeeref = employee };

        //    foreach (var employee in employees)
        //        if (employee.directreports != null)
        //        {
        //            directreport[] referencedemployees = new directreport[employee.directreports.length];
        //            int arrayindex = 0;
        //            foreach (var report in employee.directreports)
        //                referencedemployees[arrayindex++] = employeeidrefmap.first(e => e.id == report.employeeid).employeeref;

        //            employee.directreports = referencedemployees;
        //        }
        //}
    }
}
