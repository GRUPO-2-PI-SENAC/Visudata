using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class MachineRepository : BaseRepository<Machine>, IMachineRepository
{
    public MachineRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async override Task<IEnumerable<Machine>> GetAll()
    {
        return _context.Machines.Include(machine => machine.Category).Include(machine => machine.Enterprise).AsEnumerable();
    }

    public async Task<List<Machine>> GetMachinesByEnterpriseCnpj(string enterpriseCnpj)
    {
        List<Machine> machinesInDb = _context.Machines.Include(machine => machine.Enterprise).ToList();
        List<Machine> machinesOfEnterpriseFromCnpj = machinesInDb.Where(machine => machine.Enterprise.Cnpj == enterpriseCnpj).ToList();

        return machinesOfEnterpriseFromCnpj;
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

    public async override Task<Machine> GetById(int entityId)
    {
        return _context.Machines.Include(machine => machine.Enterprise).Include(machine => machine.Category).ToList().First(machine => machine.Id == entityId);
    }
}