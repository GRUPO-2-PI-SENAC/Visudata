using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class EnterpriseRepository : BaseRepository<Enterprise>, IEnterpriseRepository
{
    public EnterpriseRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<IEnumerable<Enterprise>> GetAllWithRelationships()
    {
        List<Enterprise> enterprises = _context.Enterprises.Include(enterprise => enterprise.EnterpriseMachineCategories).Include(enterprise => enterprise.Machines).Include(enterprise => enterprise.EnterpriseStatus).ToList();
        return enterprises;
    }

    public bool Login(string username, string password) => GetAll().
                            Result.
                            Where(enterprises => enterprises.Cnpj == username
                             && enterprises.Password == password).Any();


}