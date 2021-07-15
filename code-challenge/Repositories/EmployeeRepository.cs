﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRepository(ILogger<IEmployeeRepository> logger, ApplicationContext applicationContext)
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

<<<<<<< HEAD:code-challenge/Repositories/EmployeeRepository.cs
        public Employee GetDirectReports(string id)
        {
            return _applicationContext.Employees.Include(w => w.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
=======
        public List<Employee> GetDirectReports(string id)
        {
            return (List<Employee>)_applicationContext.Employees.Select(c => c.DirectReports);
>>>>>>> 7f79f51b7f3d37424ce24fafc49df9e2157f4fb2:code-challenge/Repositories/EmployeeRespository.cs
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
