using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class OutlierRegisterRepository : BaseRepository<OutlierRegister> , IOutlierRegisterRepository
{
    public OutlierRegisterRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<IEnumerable<OutlierRegister>?> GetOutlierRegistersByEnterpriseId(int enterpriseId)
    {
        List<Machine>? machinesByEnterprise = _context.Machines.Include(machine => machine.Enterprise).Where(machine => machine.Enterprise.Id == enterpriseId).ToList();
        List<OutlierRegister>? outlierRegistersInDb = _context.OutlierRegisters.Include(outlierRegister => outlierRegister.Machine).ToList();

        List<OutlierRegister>? outlierRegistersOfEnterprise = new List<OutlierRegister>();

        foreach(OutlierRegister register in outlierRegistersInDb)
        {
            if(machinesByEnterprise.Any(machine => machine.Id == register.Machine.Id))
            {
                outlierRegistersOfEnterprise.Add(register);
            }
        }

        return outlierRegistersOfEnterprise;
    }
}