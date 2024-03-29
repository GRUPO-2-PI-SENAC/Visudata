﻿using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class LogsRepository : BaseRepository<Log>, ILogsRepository
{
    public LogsRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {

    }

    public async override Task<IEnumerable<Log>> GetAll()
    {
        return _context.Logs.Include(log => log.Machine).ToList();
    }

    public async Task<Log> CurrentlyLogOfMachine(int machineId)
    {
        List<Log> logsInDb = _context.Logs.Where(log => log.Machine.Id == machineId).OrderBy(log => log.Created_at).ToList();
        return logsInDb.FirstOrDefault();
    }

    public async Task<IEnumerable<Log>> GetLogsWithMachines()
    {
        return _context.Logs.Include(log => log.Machine);
    }
}