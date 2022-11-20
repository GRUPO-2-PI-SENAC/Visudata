﻿using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Machine;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace PI.Application.Services;

public class MachineServices : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly ILogsRepository _logRepository;
    private readonly IMachineCategoryRepository _categoryRepository;
    private readonly IMachineStatusRepository _machineStatusRepository;

    public MachineServices(IMachineRepository machineRepository, IEnterpriseRepository enterpriseRepository,
        ILogsRepository logRepository, IMachineCategoryRepository categoryRepository,
        IMachineStatusRepository machineStatusRepository)
    {
        _machineRepository = machineRepository;
        _enterpriseRepository = enterpriseRepository;
        _logRepository = logRepository;
        _categoryRepository = categoryRepository;
        _machineStatusRepository = machineStatusRepository;
    }

    public async Task<List<MachinesForListViewModel>> GetAll(int enterpriseId)
    {
        bool any = _enterpriseRepository.GetAll().Result.Any(enterprise => enterprise.Id == enterpriseId);

        if (any)
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();

            IEnumerable<Machine> machines = _machineRepository.GetAll().Result
                .Where(machine => machine.Enterprise.Id == enterpriseId);

            List<MachinesForListViewModel> machinesforView = new List<MachinesForListViewModel>();

            foreach (var machine in machines)
            {
                Log logOfMachine = _logRepository.GetAll().Result
                    .Where(log => log.Machine.Id == machine.Id).MaxBy(log => log.Created_at);

                machinesforView.Add(new MachinesForListViewModel()
                {
                    Id = machine.Id,
                    Brand = machine.Brand,
                    Model = machine.Model,
                    SerialNumber = machine.SerialNumber,
                    Status = machine.Status.Name,
                    Noise = logOfMachine.Noise,
                    Temp = logOfMachine.Temp,
                    Vibration = logOfMachine.Vibration,
                    category = machine.Category.Name
                });
            }

            return machinesforView;
        }

        return new List<MachinesForListViewModel>();
    }

    public async Task<List<MachinesForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId,
        string categoryName)
    {
        List<MachinesForListViewModel> task = await GetAll(enterpriseId);

        List<MachinesForListViewModel> machinesWithCateogry =
            task.Where(machine => machine.category == categoryName).ToList();

        return machinesWithCateogry;
    }

    public async Task<bool> Add(AddMachineViewModel model, string enterpriseCnpj)
    {
        try
        {
            Machine byId = await _machineRepository.GetById(model.Id);

            if (byId != null) return false;

            Machine machineForAddInDb = new Machine()
            {
                Brand = model.Brand,
                Id = model.Id,
                Category = await AddCategoryInMachine(model.Category),
                TempMax = model.MaxTemp,
                TempMim = model.MimTemp,
                NoiseMax = model.MaxNoise,
                NoiseMin = model.MimNoise,
                VibrationMax = model.MaxVibration,
                VibrationMin = model.MimVibration,
                Created_at = DateTime.Now,
                Status = AddStatusInMachine(model.Status),
                Enterprise = await _enterpriseRepository.GetEnterpriseByCnpj(enterpriseCnpj),
                Location = model.Location
            };

            await _machineRepository.Add(machineForAddInDb);
            return true;

        }
        catch
        {
            return false;
        }
    }

    private MachineStatus AddStatusInMachine(string modelStatus)
    {
        MachineStatus? firstOrDefault = _machineStatusRepository.GetAll().Result
            .FirstOrDefault(machineStatus => machineStatus.Name == modelStatus);

        return firstOrDefault;
    }

    private async Task<MachineCategory> AddCategoryInMachine(string modelCategory)
    {
        MachineCategory byName = await _categoryRepository.GetByName(modelCategory);
        return byName;
    }

    public async Task<bool> UpdateMachine(int MachineId, AddMachineViewModel model)
    {
        try
        {
            Machine machineForUpdate = await _machineRepository.GetById(MachineId);

            machineForUpdate.NoiseMax = model.MaxNoise;
            machineForUpdate.NoiseMin = model.MimNoise;
            machineForUpdate.TempMax = model.MaxTemp;
            machineForUpdate.TempMim = model.MimTemp;
            machineForUpdate.VibrationMax = model.MaxVibration;
            machineForUpdate.VibrationMin = model.MimVibration;
            machineForUpdate.Brand = model.Brand;
            machineForUpdate.Category = await _categoryRepository.GetByName(model.Category);
            machineForUpdate.Updated_at = DateTime.Now;
            machineForUpdate.Model = model.Model;
            machineForUpdate.Location = model.Location;
            machineForUpdate.SerialNumber = model.SerialNumber;

            await _machineRepository.Update(machineForUpdate);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveMachine(int machineId)
    {
        try
        {
            Machine? machineForRemove = await _machineRepository.GetById(machineId);

            if (machineForRemove == null)
            {
                return false;
            }

            await _machineRepository.RemoveById(machineForRemove.Id);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<JsonResult> GetStatusAboutTemp(int machineId, int enterpriseId)
    {
        try
        {
            IEnumerable<Log> logsOfMachine = _logRepository.GetLogsWithMachines().Result.Where(log =>
                log.Machine.Id == machineId && log.Machine.Enterprise.Id == enterpriseId);

            IEnumerable<Log> logsAboutSixLastHours =
                logsOfMachine.Where(log => log.Created_at.Hour >= DateTime.Now.Hour - 6);

            List<KeyValuePair<int, double>> values = new List<KeyValuePair<int, double>>();

            foreach (Log log in logsAboutSixLastHours)
            {
                values.Add(new KeyValuePair<int, double>(log.Created_at.Hour, log.Temp));
            }


            return new JsonResult(values);
        }
        catch (Exception ex)
        {
            return new JsonResult("It's");
        }
    }

    public async Task<JsonResult> GetStatusAboutVibration(int machineId, int enterpriseId)
    {
        IEnumerable<Log> logsOfMachine = _logRepository.GetLogsWithMachines().Result
            .Where(log => log.Machine.Id == machineId && log.Machine.Enterprise.Id == enterpriseId);

        IEnumerable<Log> logsAboutSixHours = logsOfMachine.Where(log => log.Created_at.Hour < DateTime.Now.Hour - 6);

        List<KeyValuePair<int, double>> hourWithValueAboutVibrationOfMachine = new List<KeyValuePair<int, double>>();

        foreach (Log log in logsAboutSixHours)
        {
            hourWithValueAboutVibrationOfMachine.Add(new KeyValuePair<int, double>(log.Created_at.Hour, log.Vibration));
        }

        return new JsonResult(hourWithValueAboutVibrationOfMachine);
    }

    public async Task<JsonResult> GetStatusAboutNoise(int machineId, int enterpriseId)
    {
        IEnumerable<Log> logsOfMachine = _logRepository.GetLogsWithMachines().Result
            .Where(log => log.Machine.Id == machineId && log.Machine.Enterprise.Id == enterpriseId);

        IEnumerable<Log> logsAboutSixHours = logsOfMachine.Where(log => log.Created_at.Hour > DateTime.Now.Hour - 6);

        List<KeyValuePair<int, double>> hoursWithValueAboutNoiseOfMachine = new List<KeyValuePair<int, double>>();

        foreach (Log log in logsAboutSixHours)
        {
            hoursWithValueAboutNoiseOfMachine.Add(new KeyValuePair<int, double>(log.Created_at.Hour, log.Noise));
        }

        return new JsonResult(hoursWithValueAboutNoiseOfMachine);
    }

    public async Task<AmountOfMachineByStatusViewModel> GetAmountOfMachinesByStatusWithEnterpriseId(int enterpriseId)
    {
        try
        {
            Enterprise? enterpriseForViewMachineStatus = await _enterpriseRepository.GetById(enterpriseId);

            if (enterpriseForViewMachineStatus == null)
                return new AmountOfMachineByStatusViewModel()
                { AmountOfAttentionStatus = 0, AmountOfGoodStatus = 0, AmountOfCriticalStatus = 0 };

            IEnumerable<Machine> machinesForGroupByStatus =
                await _machineRepository.GetMachinesByEnterpriseId(enterpriseId);

            int amountOfMachinesWithGoodStatus = 0;
            int amountOfMachinesWithAttentionStatus = 0;
            int amountOfMachinesWithCriticalStatus = 0;

            machinesForGroupByStatus.ToList().ForEach(machine =>
            {
                if (machine.Status.Name == "Bom Estado")
                {
                    amountOfMachinesWithGoodStatus++;
                }
                else if (machine.Status.Name == "Alerta")
                {
                    amountOfMachinesWithAttentionStatus++;
                }
                else if (machine.Status.Name == "Estado crítico")
                {
                    amountOfMachinesWithCriticalStatus++;
                }
            });


            return new AmountOfMachineByStatusViewModel()
            {
                AmountOfGoodStatus = amountOfMachinesWithGoodStatus,
                AmountOfAttentionStatus = amountOfMachinesWithAttentionStatus,
                AmountOfCriticalStatus = amountOfMachinesWithCriticalStatus
            };
        }
        catch
        {
            return new AmountOfMachineByStatusViewModel()
            {
                AmountOfAttentionStatus = 0,
                AmountOfGoodStatus = 0,
                AmountOfCriticalStatus = 0
            };
        }
    }

    public async Task<bool> UpdateMachineStatus(int machineId, int enterpriseId, int statusId)
    {
        try
        {
            Machine? machineForUpdateStatus = _machineRepository.GetAll().Result
                .Where(machine => machine.Id == machineId && machine.Enterprise.Id == enterpriseId).FirstOrDefault();

            if (machineForUpdateStatus != null)
            {
                machineForUpdateStatus.Status = await _machineStatusRepository.GetById(statusId);

                await _machineRepository.Update(machineForUpdateStatus);

                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<List<MachinesForListViewModel>> GetMachinesByEnterpriseId(int enterpriseId)
    {
        try
        {
            List<Machine> machinesOfEnterprise = _machineRepository.GetAll().Result.Where(machine => machine.Enterprise.Id == enterpriseId).ToList();
            List<MachinesForListViewModel> modelForView = new List<MachinesForListViewModel>();

            foreach (Machine machine in machinesOfEnterprise)
            {
                Log currentlyLogOfMachine = await _logRepository.CurrentlyLogOfMachine(machine.Id);

                modelForView.Add(new MachinesForListViewModel
                {
                    Brand = machine.Brand,
                    Id = machine.Id,
                    category = machine.Category.Name,
                    Model = machine.Model,
                    Noise = currentlyLogOfMachine.Noise,
                    Temp = currentlyLogOfMachine.Temp,
                    Vibration = currentlyLogOfMachine.Vibration,
                    SerialNumber = machine.SerialNumber,
                    Status = GetMachineStatusByMachine(machine),
                });
            }

            return modelForView;
        }
        catch
        {
            return new List<MachinesForListViewModel>();
        }
    }

    private string GetMachineStatusByMachine(Machine machineForextractStatus)
    {
        MachineStatus? machineStatusOfMachineGetById = _machineStatusRepository.GetAll().Result.ToList().FirstOrDefault(machineStatus => machineStatus.Id == machineForextractStatus.Status.Id);

        if (machineStatusOfMachineGetById.Name == "Atênçäo")
            return "Atênçäo";

        if (machineStatusOfMachineGetById.Name == "Perigo")
            return "Perigo";

        else
            return "Bom";
    }

    public async Task<bool> AddRegisterOfMachineFromJson(MachineDataRecieveFromSensorsJsonModel model)
    {
        try
        {
            Machine? machineForAddLog = await _machineRepository.GetById(model.MachineId); 

            if (machineForAddLog.Equals(null))
                return false;

            await _logRepository.Add(new Log
            {
                Created_at = DateTime.Now,
                Machine = machineForAddLog,
                Noise = model.Noise,
                Temp = model.Temp,
                Vibration = model.Vibration
            });

            return true;

        }
        catch
        {
            return false;
        }

    }
}