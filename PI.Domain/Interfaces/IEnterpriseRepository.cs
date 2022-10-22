using PI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Interfaces
{
    public interface IEnterpriseRepository : IBaseRepository<Enterprise>
    {
        bool Login(string username, string password);
    }
}
