using Microsoft.AspNetCore.Mvc;
using PI.Domain.Entities;
using System.Runtime.CompilerServices;

namespace PI.Application.Intefaces;

public interface IMachineService
{
    Task<List<Machine>> GetAllByCnpj(string? enterpriseOfCurrentSessionCnpj);
    Task<List<Machine>> GetByCategory(string? enterpriseOfCurrentSessionCnpj, string nameOfCategory);
    Task<Machine> GetById(int id);
    Task<bool> Delete(int id);
    Task<bool> Add(Machine machineEntity, string enterpriseCnpj);
    Task<bool> AddRegister(int machineId, double temp, double noise, double vibration);
    Task<bool> Update(Machine machine);
    Task<List<Machine>> GetByStatus(string? enterpriseCnpj, string status);
}