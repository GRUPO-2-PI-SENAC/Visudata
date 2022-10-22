using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class OutlierRegisterRepository : BaseRepository<OutlierRegister> , IOutlierRegisterRepository
{
    public OutlierRegisterRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}