using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class EnterpriseStatusRepository : BaseRepository<EnterpriseStatus> , IEnterpriseStatusRepository
{
    public EnterpriseStatusRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<List<string>> GetNameOfAllStatus()
    {
        IEnumerable<EnterpriseStatus> enterpriseStatusInDb = await GetAll();

        List<String> nameOfEnterpriseStatus = new List<string>();

        foreach (var enterpriseStatus in enterpriseStatusInDb)
        {
            nameOfEnterpriseStatus.Add(enterpriseStatus.Name);
        }

        return nameOfEnterpriseStatus;
    }
}