﻿using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Create(Compensation compensation);

        Compensation GetById(string id);

        Task SaveAsync();

    }
}
