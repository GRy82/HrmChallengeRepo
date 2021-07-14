using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _applicationContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _applicationContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _applicationContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _applicationContext.Employees.Remove(employee).Entity;
        }
    }
}
