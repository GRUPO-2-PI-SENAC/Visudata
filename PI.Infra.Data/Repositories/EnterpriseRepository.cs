using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class EnterpriseRepository : BaseRepository<Enterprise>, IEnterpriseRepository
{
    public EnterpriseRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
    
    public bool Login(string username, string password) => GetAll().
                            Result.
                            Where(enterprises => enterprises.Cnpj == username 
                             && enterprises.Password == password).Any();
    
    
}