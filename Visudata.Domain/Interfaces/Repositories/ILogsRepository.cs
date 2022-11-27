using PI.Domain.Entities;

namespace PI.Domain.Interfaces.Repositories
{
    public interface ILogsRepository : IBaseRepository<Log>
    {
        Task<IEnumerable<Log>> GetLogsWithMachines();
        Task<Log> CurrentlyLogOfMachine(int machineId);
    }
}
