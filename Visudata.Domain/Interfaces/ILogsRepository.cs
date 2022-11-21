using PI.Domain.Entities;

namespace PI.Domain.Interfaces
{
    public interface ILogsRepository : IBaseRepository<Log>
    {
        Task<IEnumerable<Log>> GetLogsWithMachines();
        Task<Log> CurrentlyLogOfMachine(int machineId); 
    }
}
