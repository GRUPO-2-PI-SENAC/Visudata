using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class LogsRepository : BaseRepository<Log>, ILogsRepository
{
    public LogsRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {

    }

    public async Task<Log> CurrentlyLogOfMachine(int machineId)
    {
        List<Log> logsInDb = _context.Logs.ToList();
        List<Log> logsOfMachinesGroupByDateTime = logsInDb.Where(log => log.Machine.Id == machineId).OrderBy(log => log.Created_at).ToList();

        return logsOfMachinesGroupByDateTime.FirstOrDefault();
    }

    public async Task<IEnumerable<Log>> GetLogsWithMachines()
    {
        return _context.Logs.Include(log => log.Machine);
    }
}