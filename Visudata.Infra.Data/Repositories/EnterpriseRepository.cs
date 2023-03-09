using Dapper;
using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class EnterpriseRepository : BaseRepository<Enterprise>, IEnterpriseRepository
{
    public EnterpriseRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<IEnumerable<Enterprise>> GetAllWithRelationships()
    {
        return await Task.Run(async () =>
        {
            string query = @"select * from enterprises inner join machines m on enterprises.Id = m.EnterpriseId;";
            IEnumerable<Enterprise> enterprises = await _databaseConnection.QueryAsync<Enterprise, IEnumerable<Machine>, Enterprise>(query,
                (enterprise, machines) =>
                {
                    enterprise.Machines = machines;
                    return enterprise;
                });
            return enterprises;
        });
    }

    public async Task<Enterprise> GetEnterpriseByCnpj(string cnpj)
    {
        return await Task.Run(async () =>
        {
            string query = @"select * from enterprises inner join machines m on enterprises.Id = m.EnterpriseId where enterprises.Cnpj = @enterpriseCnpj";
            Enterprise fromCnpj = (await _databaseConnection.QueryAsync<Enterprise, IEnumerable<Machine>, Enterprise>(query,
                (enterprise, machines) =>
                {
                    enterprise.Machines = machines;
                    return enterprise;
                },
                new
                {
                    enterpriseCnpj = cnpj
                }
                )).First();

            return fromCnpj;
        });
    }

    public Enterprise GetEnterpriseByIdWithoutAsync(int enterpriseId)
    {
        return _context.Enterprises.FirstOrDefault(enterprise => enterprise.Id == enterpriseId);
    }

    public bool Login(string username, string password) => GetAll().
                            Result.
                            Where(enterprises => enterprises.Cnpj == username
                             && enterprises.Password == password).Any();


}