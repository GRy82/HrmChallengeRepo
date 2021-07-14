using challenge.Data;
using challenge.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ApplicationContext applicationContext, ILogger<ICompensationRepository> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }
        public Compensation Create(Compensation compensation)
        {
            _applicationContext.Compensation.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            return _applicationContext.Compensation.SingleOrDefault(c => c.EmployeeId == id);
        }
    }
}
