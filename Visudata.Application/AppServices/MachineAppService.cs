using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace PI.Application.AppServices
{
    public class MachineAppService : IMachineAppService
    {
        private readonly IMachineService _machineService;

        public MachineAppService(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public Task<bool> Add(Machine machineEntity, string enterpriseCnpj)
        {
            return _machineService.Add(machineEntity, enterpriseCnpj);
        }

        public Task<bool> AddRegister(int machineId, double temp, double noise, double vibration)
        {
            return _machineService.AddRegister(machineId, temp, noise, vibration);
        }

        public Task<bool> Delete(int id)
        {
            return _machineService.Delete(id);
        }

        public Task<Machine> GetById(int id)
        {
            return _machineService.GetById(id);
        }

        public Task<List<Machine>> GetByStatus(string? enterpriseCnpj, string status)
        {
            return _machineService.GetByStatus(enterpriseCnpj, status);
        }

        public Task<List<Machine>> GetAllByCnpj(string? enterpriseOfCurrentSessionCnpj)
        {
            return _machineService.GetAllByCnpj(enterpriseOfCurrentSessionCnpj);
        }

        public Task<List<Machine>> GetAllByCategory(string? enterpriseOfCurrentSessionCnpj, string nameOfCategory)
        {
            return _machineService.GetByCategory(enterpriseOfCurrentSessionCnpj, nameOfCategory);
        }

        public Task<bool> Update(Machine machine)
        {
            return _machineService.Update(machine);
        }
    }
}
