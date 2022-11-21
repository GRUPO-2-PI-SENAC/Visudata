using PI.Domain.Entities;

namespace PI.Domain.Interfaces
{
    public interface IEnterpriseRepository : IBaseRepository<Enterprise>
    {
        bool Login(string username, string password);
        Task<IEnumerable<Enterprise>> GetAllWithRelationships();
        Enterprise GetEnterpriseByIdWithoutAsync(int enterpriseId);
        Task<Enterprise> GetEnterpriseByCnpj(string cnpj); 
    }
}
