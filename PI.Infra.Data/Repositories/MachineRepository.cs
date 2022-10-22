using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class MachineRepository : BaseRepository<Machine> , IMachineRepository
{
    public MachineRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}