using PI.Domain.Entities;

namespace PI.Domain.Interfaces
{
    public interface IOutlierRegisterRepository : IBaseRepository<OutlierRegister>
    {
        Task<IEnumerable<OutlierRegister>> GetOutlierRegistersByEnterpriseId(int enterpriseId);
    }
}
