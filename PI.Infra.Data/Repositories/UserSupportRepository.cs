using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Infra.Data.Repositories
{
    public class UserSupportRepository : BaseRepository<UserSupport>, IUserSupportRepository
    {
        public UserSupportRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
