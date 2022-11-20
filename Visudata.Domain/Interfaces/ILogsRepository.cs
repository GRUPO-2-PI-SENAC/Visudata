﻿using PI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Interfaces
{
    public interface ILogsRepository : IBaseRepository<Log>
    {
        Task<IEnumerable<Log>> GetLogsWithMachines();
        Task<Log> CurrentlyLogOfMachine(int machineId); 
    }
}
