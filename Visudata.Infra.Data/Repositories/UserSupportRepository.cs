using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories
{
    public class UserSupportRepository : BaseRepository<UserSupport>, IUserSupportRepository
    {
        public UserSupportRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
        {
        }
    }
}
