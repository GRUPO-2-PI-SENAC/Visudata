using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace PI.Application.Services;

public class MachineServices : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly ILogsRepository _logRepository;

    public MachineServices(IMachineRepository machineRepository, IEnterpriseRepository enterpriseRepository,
        ILogsRepository logRepository)
    {
        _machineRepository = machineRepository;
        _enterpriseRepository = enterpriseRepository;
        _logRepository = logRepository;
    }


    public async Task<List<Machine>> GetMachinesForSpecificCategory(int enterpriseId,
        string categoryName)
    {
        Enterprise enterprise = await _enterpriseRepository.GetById(enterpriseId);

        List<Machine> machines = (await GetAllByCnpj(enterprise.Cnpj)).Where(machine => machine.Category.Name == categoryName).ToList();

        return machines;
    }

    public Task<List<Machine>> GetByCategory(string enterpriseOfCurrentSessionCnpj, string nameOfCategory)
    {
        return _machineRepository.GetAllByCnpjAndCategory(enterpriseOfCurrentSessionCnpj, nameOfCategory);
    }

    public Task<List<Machine>> GetAllByCnpj(string enterpriseOfCurrentSessionCnpj)
    {
        return _machineRepository.GetMachinesByEnterpriseCnpj(enterpriseOfCurrentSessionCnpj);
    }

    public Task<Machine> GetById(int id)
    {
        return _machineRepository.GetById(id);
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            Machine machineForDelete = await _machineRepository.GetById(id);

            await _machineRepository.Delete(id);

            return true;

        }
        catch
        {
            return false;
        }
    }

    public Task<bool> Add(Machine machineEntity, string enterpriseCnpj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRegister(int machineId, double temp, double noise, double vibration)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Machine machine)
    {
        throw new NotImplementedException();
    }

    public Task<List<Machine>> GetByStatus(string? enterpriseCnpj, string status)
    {
        throw new NotImplementedException();
    }

}