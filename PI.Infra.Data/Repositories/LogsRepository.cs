using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class LogsRepository : BaseRepository<Log>, ILogsRepository
{
    public LogsRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}