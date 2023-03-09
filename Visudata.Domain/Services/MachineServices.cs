using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Routing.Constraints;

namespace PI.Application.Services;

public class MachineServices : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly ILogsRepository _logRepository;
    private readonly IMachineCategoryRepository _machineCategoryRepository;
    private readonly IOutlierRegisterRepository _outlierRegisterRepository;

    public MachineServices(IMachineRepository machineRepository, IEnterpriseRepository enterpriseRepository,
        ILogsRepository logRepository, IMachineCategoryRepository machineCategoryRepository, IOutlierRegisterRepository outlierRegisterRepository)
    {
        _machineRepository = machineRepository;
        _enterpriseRepository = enterpriseRepository;
        _logRepository = logRepository;
        _machineCategoryRepository = machineCategoryRepository;
        _outlierRegisterRepository = outlierRegisterRepository;
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

    public async Task<bool> Add(Machine machineEntity, string enterpriseCnpj)
    {
        try
        {
            Enterprise enterprise = await _enterpriseRepository.GetEnterpriseByCnpj(enterpriseCnpj);
            machineEntity.Enterprise = enterprise;
            machineEntity.Category = await _machineCategoryRepository.GetByName(machineEntity.Category.Name);
            await _machineRepository.Add(machineEntity);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> AddRegister(int machineId, double temp, double noise, double vibration)
    {
        try
        {

            Machine machineForAddLog = await _machineRepository.GetById(machineId);
            OutlierRegister register = new();
            Log entity = new Log()
            {
                Created_at = DateTime.Now,
                Machine = machineForAddLog,
                Noise = noise,
                Temp = temp,
                Vibration = vibration
            };

            if (await VerifyOutlierRegister(machineForAddLog, vibration, noise, temp))
            {
                register.Machine = machineForAddLog;
                register.Temp = temp;
                register.Noise = noise;
                register.Vibration = vibration;
                register.Time = DateTime.Now;
                register.Created_at = DateTime.Now;

                await _outlierRegisterRepository.Add(register);
                entity.OutlierRegister = register;
            }

            await _logRepository.Add(entity);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Update(Machine machine)
    {
        try
        {
            Machine forUpdate = await _machineRepository.GetById(machine.Id);
            forUpdate.Category = await _machineCategoryRepository.GetByName(machine.Category.Name);
            forUpdate.NoiseMax = machine.NoiseMax;
            forUpdate.NoiseMin = machine.NoiseMin;
            forUpdate.TempMax = machine.TempMax;
            forUpdate.TempMin = machine.TempMin;
            forUpdate.VibrationMax = machine.VibrationMax;
            forUpdate.VibrationMin = machine.VibrationMin;
            forUpdate.Brand = machine.Brand;
            forUpdate.Model = machine.Model;
            forUpdate.SerialNumber = machine.SerialNumber;
            forUpdate.Tag = machine.Tag;
            forUpdate.Updated_at = DateTime.Now;

            await _machineRepository.Update(forUpdate);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<Machine>> GetByStatus(string? enterpriseCnpj, string status)
    {
        List<Machine> machines = (await _machineRepository.GetAll()).ToList();

        return machines.Where(machine => machine.Enterprise.Cnpj == enterpriseCnpj).ToList();
    }

    private async Task<bool> VerifyOutlierRegister(Machine machineForVerify, double vibration, double noise, double temp)
    {
        if (machineForVerify.NoiseMax < noise || machineForVerify.NoiseMin > noise ||
            machineForVerify.TempMax < temp || machineForVerify.TempMin > temp ||
            machineForVerify.VibrationMax < vibration || machineForVerify.VibrationMin > vibration)
            return true;

        return false;

    }

    public async Task<List<Machine>> GetAll()
    {
        IEnumerable<Machine> machineFromRepositoryAsEnumerable = await _machineRepository.GetAll();
        return machineFromRepositoryAsEnumerable.ToList();
    }
}