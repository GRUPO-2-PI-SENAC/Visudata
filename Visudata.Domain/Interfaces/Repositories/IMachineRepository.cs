using PI.Domain.Entities;

namespace PI.Domain.Interfaces.Repositories
{
    public interface IMachineRepository : IBaseRepository<Machine>
    {
        Task<IEnumerable<Machine>> GetMachinesWithRelationShips();
        Task<IEnumerable<Machine>> GetMachinesByEnterpriseId(int enterpriseId);
        Task<List<Machine>> GetMachinesByEnterpriseCnpj(string enterpriseCnpj);
        Task<List<Machine>> GetAllByCnpjAndCategory(string cnpj, string category);
    }
}
