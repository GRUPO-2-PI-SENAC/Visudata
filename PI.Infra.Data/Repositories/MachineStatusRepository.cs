using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class MachineStatusRepository : BaseRepository<MachineStatus>, IMachineStatusRepository
{
    public MachineStatusRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}