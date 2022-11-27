using PI.Domain.Entities;

namespace PI.Domain.Interfaces.Repositories;

public interface IEnterpriseStatusRepository : IBaseRepository<EnterpriseStatus>
{
    Task<List<string>> GetNameOfAllStatus();
}