using PI.Domain.Entities;

namespace PI.Domain.Interfaces;

public interface IEnterpriseStatusRepository : IBaseRepository<EnterpriseStatus>
{
    Task<List<String>> GetNameOfAllStatus();
}