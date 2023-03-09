using Dapper;
using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories
{
    public class MachineCategoryRepository : BaseRepository<MachineCategory>, IMachineCategoryRepository
    {
        public MachineCategoryRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
        {
        }

        public async Task<MachineCategory> GetByName(string name)
        {
            return await Task.Run(async () =>
            {
                var query = @"select * from machine_category inner join machines m on machine_category.Id = m.CategoryId where machine_category.Name = @categoryName;";
                MachineCategory machineCategoryByName = (await _databaseConnection.QueryAsync<MachineCategory, IEnumerable<Machine>, MachineCategory>(query
                    , (mc, machines) =>
                    {
                        mc.Machines = machines;
                        return mc;
                    },
                    new
                    {
                        @categoryName = name
                    }
                    ))
                    .First();

                return machineCategoryByName;
            });
        }

        public async Task<IEnumerable<Machine>> GetMachinesOfCategory(int categoryId)
        {
            return await Task.Run(async () =>
            {
                var query = @"select * from machines inner join machine_category mc on machines.CategoryId = mc.Id where mc.Id = @idcategory;";

                IEnumerable<Machine> machinesFromCategory = await _databaseConnection.QueryAsync<Machine, MachineCategory, Machine>(query,
                    (machine, machineCategory) =>
                    {
                        machine.Category = machineCategory;
                        return machine;
                    },
                    new
                    {
                        idcategory = categoryId
                    });

                return machinesFromCategory;

            });
        }
    }
}
