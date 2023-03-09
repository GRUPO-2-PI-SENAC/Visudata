using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        return await Task.Run(async () =>
        {
            var query = @"select * from machines inner join enterprises on machines.EnterpriseId = enterprises.Id
            inner join machine_category mc on machines.CategoryId = mc.Id";

            IEnumerable<Machine> machinesAsEnumerable = await _databaseConnection.QueryAsync<Machine, Enterprise, MachineCategory, Machine>(query, map: (machineEntity, enterpriseEntity, categoryEntity) =>
            {
                machineEntity.Category = categoryEntity;
                machineEntity.Enterprise = enterpriseEntity;

                return machineEntity;
            });
            return machinesAsEnumerable;
        });
    }

    public async Task<List<Machine>> GetMachinesByEnterpriseCnpj(string enterpriseCnpj)
    {
        return await Task.Run(async () =>
        {
            var query = @"select * from machines inner join enterprises e on machines.EnterpriseId = e.Id 
            inner join machine_category mc on machines.CategoryId = mc.Id where e.Cnpj = @cnpj;";

            IEnumerable<Machine> machinesAsEnumerable = await _databaseConnection.QueryAsync<Machine, Enterprise, MachineCategory, Machine>(query, map: (machine, enterprise, category) =>
            {
                machine.Enterprise = enterprise;
                machine.Category = category;
                return machine;
            },
            new { cnpj = enterpriseCnpj }
            );
            return machinesAsEnumerable.ToList();
        });
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

    public async override Task<Machine> GetById(int machineId)
    {
        return await Task.Run(async () =>
        {
            var query = @"select * from machines inner join enterprises e on machines.EnterpriseId = e.Id
            inner join machine_category mc on machines.CategoryId = mc.Id where machines.Id = @id;";

            Machine machines = (await _databaseConnection.QueryAsync<Machine, Enterprise, MachineCategory, Machine>(query,
                (machine, enterprise, category) =>
                {
                    machine.Enterprise = enterprise;
                    machine.Category = category;
                    return machine;
                },
                new { id = machineId }
                )).First();
            return machines;
        });
    }

    public async Task<List<Machine>> GetAllByCnpjAndCategory(string cnpj, string category)
    {
        return await Task.Run(async () =>
        {
            var query = @"select * from machines inner join machine_category mc on machines.CategoryId = mc.Id 
            inner join enterprises e on machines.EnterpriseId = e.Id where e.Cnpj = @enterpriseCnpj and mc.Name = @categoryName";

            IEnumerable<Machine> machinesAsIEnumerable = await _databaseConnection.QueryAsync<Machine, Enterprise, MachineCategory, Machine>(query,
                (machine, enterprise, category) =>
                {
                    machine.Enterprise = enterprise;
                    machine.Category = category;
                    return machine;
                },
                new
                {
                    enterpriseCnpj = cnpj,
                    categoryName = category
                });

            return machinesAsIEnumerable.ToList();
        });
    }
}