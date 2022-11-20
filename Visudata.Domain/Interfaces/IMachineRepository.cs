using PI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Interfaces
{
    public interface IMachineRepository : IBaseRepository<Machine>
    {
        Task<IEnumerable<Machine>> GetMachinesWithRelationShips();
        Task<IEnumerable<Machine>> GetMachinesByEnterpriseId(int enterpriseId);
        Task<List<Machine>> GetMachinesByEnterpriseCnpj(string enterpriseCnpj);
    }
}
