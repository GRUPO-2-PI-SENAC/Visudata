using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class MachineRepository : BaseRepository<Machine> , IMachineRepository
{
    public MachineRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<IEnumerable<Machine>> GetMachinesByEnterpriseId(int enterpriseId)
    {
        List<Machine> machinesOfEnterprise = _context.Machines.Include(machine => machine.Enterprise).Where(machine => machine.Enterprise.Id == enterpriseId).ToList();

        return machinesOfEnterprise;
    }

    public async Task<IEnumerable<Machine>> GetMachinesWithRelationShips()
    {
        return _context.Machines.Include(machine => machine.Category).Include(machine => machine.Enterprise).Include(machine => machine.Status).ToList();
    }
}