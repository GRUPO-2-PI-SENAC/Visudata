using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class MachineStatusRepository : BaseRepository<MachineStatus>, IMachineStatusRepository
{
    public MachineStatusRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<IEnumerable<Machine>?> GetMachinesByStatus(int statusId)
    {
        List<Machine>? machinesByStatus = _context.Machines.Include(machine => machine.Status).Where(machine => machine.Status.Id == statusId).ToList();

        return machinesByStatus;
    }
}