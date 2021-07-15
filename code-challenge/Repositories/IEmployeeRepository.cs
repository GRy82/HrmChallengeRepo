using challenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
<<<<<<< HEAD
        Employee GetDirectReports(String id);
=======
        List<Employee> GetDirectReports(String id);
>>>>>>> 7f79f51b7f3d37424ce24fafc49df9e2157f4fb2
        Task SaveAsync();
    }
}