using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{

    // The file and class were renamed to reflect that it establishes DbSets for more than just Employee objects. This way,
    // no additional instances of DbContext need to be registered at startup.
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Compensation> Compensation { get; set; }
    }
}
