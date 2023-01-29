using Microsoft.AspNetCore.Mvc;
using PI.Domain.Entities;

namespace PI.Application.Intefaces
{
    public interface IMachineAppService
    {
        Task<bool> Add(Machine machineEntity, string enterpriseCnpj);
        Task<bool> AddRegister(int machineId, double temp, double noise, double vibration);
        Task<bool> Delete(int id);
        Task<Machine> GetById(int id);
        Task<List<Machine>> GetByStatus(string? enterpriseOfCurrentSessionCnpj, string status);

        Task<List<Machine>> GetAllByCnpj(string? enterpriseOfCurrentSessionCnpj);
        Task<List<Machine>> GetAllByCategory(string? enterpriseOfCurrentSessionCnpj, string nameOfCategory);
        Task<bool> Update(Machine machine);
    }
}
