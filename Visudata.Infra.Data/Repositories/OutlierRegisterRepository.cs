using Dapper;
using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class OutlierRegisterRepository : BaseRepository<OutlierRegister>, IOutlierRegisterRepository
{
    public OutlierRegisterRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<IEnumerable<OutlierRegister>?> GetOutlierRegistersByEnterpriseId(int enterpriseId)
    {

        return await Task.Run(async () =>
        {
            var query = @"select * from machines inner join machine_category mc on machines.CategoryId = mc.Id inner join logs l on machines.Id = l.MachineId
            inner join outlierregisters o on l.OutlierRegisterId = o.Id where machines.EnterpriseId = @identerprise;";
            IEnumerable<OutlierRegister> outlierRegistersOfCurrentEnterprise = await _databaseConnection.QueryAsync<OutlierRegister, Machine, MachineCategory, Log, OutlierRegister>(query,
                (outlierRegister, machine, machineCategory, log) =>
                {
                    outlierRegister.Machine = machine;
                    outlierRegister.Machine.Category = machineCategory;
                    return outlierRegister;
                },
                new
                {
                    identerprise = enterpriseId,
                }
                );

            return outlierRegistersOfCurrentEnterprise;
        });
    }
}