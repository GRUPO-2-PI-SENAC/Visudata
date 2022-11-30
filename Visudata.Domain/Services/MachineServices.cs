using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Domain.ViewModel.Machine;

namespace PI.Application.Services;

public class MachineServices : IMachineService
{
    private readonly IMachineRepository _machineRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly ILogsRepository _logRepository;
    private readonly IMachineCategoryRepository _categoryRepository;

    public MachineServices(IMachineRepository machineRepository, IEnterpriseRepository enterpriseRepository,
        ILogsRepository logRepository, IMachineCategoryRepository categoryRepository)
    {
        _machineRepository = machineRepository;
        _enterpriseRepository = enterpriseRepository;
        _logRepository = logRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<MachineForListViewModel>> GetAll(int enterpriseId)
    {
        bool any = _enterpriseRepository.GetAll().Result.Any(enterprise => enterprise.Id == enterpriseId);

        if (any)
        {
            IEnumerable<Log> logs = await _logRepository.GetAll();

            IEnumerable<Machine> machines = _machineRepository.GetAll().Result
                .Where(machine => machine.Enterprise.Id == enterpriseId);

            List<MachineForListViewModel> machinesforView = new List<MachineForListViewModel>();

            foreach (Machine machine in machines)
            {
                Log logOfMachine = _logRepository.GetAll().Result
                    .Where(log => log.Machine.Id == machine.Id).MaxBy(log => log.Created_at);

                machinesforView.Add(new MachineForListViewModel()
                {
                    Id = machine.Id,
                    Brand = machine.Brand,
                    Model = machine.Model,
                    SerialNumber = machine.SerialNumber,
                    Status = GetStatusForViewFromMachineStatusEnum(machine.Status),
                    Noise = logOfMachine.Noise,
                    Temp = logOfMachine.Temp,
                    Vibration = logOfMachine.Vibration,
                    category = machine.Category.Name
                });
            }

            return machinesforView;
        }

        return new List<MachineForListViewModel>();
    }

    public async Task<List<MachineForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId,
        string categoryName)
    {
        List<MachineForListViewModel> task = await GetAll(enterpriseId);

        List<MachineForListViewModel> machinesWithCateogry =
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
                Tag = model.Tag,
                SerialNumber = model.SerialNumber,
                Category = await AddCategoryInMachine(model.Category),
                Model = model.Model,
                Created_at = DateTime.Now,
                Status = MachineStatus.Undefined,
                Enterprise = await _enterpriseRepository.GetEnterpriseByCnpj(enterpriseCnpj),
                NoiseMax = model.MaxNoise,
                NoiseMin = model.MinNoise,
                VibrationMax = model.MaxVibration,
                VibrationMin = model.MinVibration,
                TempMin = model.MinTemp,
                TempMax = model.MaxTemp,
            };

            await _machineRepository.Add(machineForAddInDb);
            return true;

        }
        catch
        {
            return false;
        }
    }

    private async Task<MachineCategory> AddCategoryInMachine(string modelCategory)
    {
        IEnumerable<MachineCategory> machineCategoriesInDb = await _categoryRepository.GetAll();
        MachineCategory machineCategoryForAddInmachine = machineCategoriesInDb.FirstOrDefault(machineCategory =>
            machineCategory.Name.ToLower() == modelCategory.ToLower());

        return machineCategoryForAddInmachine;
    }

    public async Task<bool> UpdateMachine(EditMachineViewModel model)
    {
        try
        {
            Machine machineForUpdate = await _machineRepository.GetById(model.MachineId);

            machineForUpdate.NoiseMax = model.NoiseMax;
            machineForUpdate.NoiseMin = model.NoiseMin;
            machineForUpdate.TempMax = model.TempMax;
            machineForUpdate.TempMin = model.TempMin;
            machineForUpdate.VibrationMax = model.VibrationMax;
            machineForUpdate.VibrationMin = model.VibrationMin;
            machineForUpdate.Brand = model.Brand;
            machineForUpdate.Category = await _categoryRepository.GetByName(model.Category);
            machineForUpdate.Updated_at = DateTime.Now;
            machineForUpdate.Model = model.Model;
            machineForUpdate.Tag = model.Tag;
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

            IEnumerable<Machine> machineFromEnterprise =
                await _machineRepository.GetMachinesByEnterpriseId(enterpriseId);

            int amountOfMachinesWithGoodStatus = machineFromEnterprise.Where(machine => machine.Status == MachineStatus.Good).Count();
            int amountOfMachinesWithAttentionStatus = machineFromEnterprise.Where(machine => machine.Status == MachineStatus.Warning).Count();
            int amountOfMachinesWithCriticalStatus = machineFromEnterprise.Where(machine => machine.Status == MachineStatus.Critical).Count();


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

    public async Task<List<MachineForListViewModel>> GetMachinesByEnterpriseId(int enterpriseId)
    {
        try
        {
            List<Machine> machinesOfEnterprise = _machineRepository.GetAll().Result.Where(machine => machine.Enterprise.Id == enterpriseId).ToList();
            List<MachineForListViewModel> modelForView = new List<MachineForListViewModel>();

            foreach (Machine machine in machinesOfEnterprise)
            {
                Log currentlyLogOfMachine = await _logRepository.CurrentlyLogOfMachine(machine.Id);

                List<Log> lastLogsAbout6HoursOfMachine = _logRepository.GetAll().Result
                    .Where(log => log.Machine.Id == machine.Id)
                    .Where(log => log.Created_at.Hour > DateTime.Now.Hour - 6).ToList();

                MachineForListViewModel model = new MachineForListViewModel()
                {
                    Brand = machine.Brand,
                    Id = machine.Id,
                    category = machine.Category.Name,
                    Model = machine.Model,
                    Noise = currentlyLogOfMachine.Noise,
                    Temp = currentlyLogOfMachine.Temp,
                    Vibration = currentlyLogOfMachine.Vibration,
                    SerialNumber = machine.SerialNumber,
                    Status = GetStatusForViewFromMachineStatusEnum(machine.Status),
                };
                List<Log> logsWithVibration = lastLogsAbout6HoursOfMachine.Where(log => log.Vibration > machine.VibrationMax || log.Vibration < machine.VibrationMin).ToList();
                List<Log> logsWithNoise = lastLogsAbout6HoursOfMachine.Where(log => log.Noise > machine.NoiseMax || log.Vibration < machine.NoiseMin).ToList();
                List<Log> logsWithTemp = lastLogsAbout6HoursOfMachine.Where(log => log.Temp > machine.TempMax || log.Temp < machine.TempMin).ToList();

                if (logsWithNoise.Any())
                {
                    model.NoiseStyle = logsWithNoise.Count() > 1 ? "redBt" : "yellowBt";
                }
                else
                    model.NoiseStyle = "greenBt";

                if (logsWithVibration.Any())
                {
                    model.VibrationStyle = logsWithVibration.Count() > 1 ? "redBt" : "yellowBt";
                }
                else
                    model.VibrationStyle = "greenBt";

                if (logsWithTemp.Any())
                {
                    model.TempStyle = logsWithTemp.Count() > 1 ? "redBt" : "yellowBt";
                }
                else
                    model.TempStyle = "greenBt";


                modelForView.Add(model);
            }

            return modelForView;
        }
        catch
        {
            return new List<MachineForListViewModel>();
        }
    }

    public async Task<bool> AddRegisterOfMachineFromJson(MachineDataRecieveFromSensorsJsonModel model)
    {
        try
        {
            Machine? machineForAddLog = await _machineRepository.GetById(model.MachineId);

            if (machineForAddLog.Equals(null))
                return false;

            if (machineForAddLog.NoiseMax < model.Noise || machineForAddLog.NoiseMin > model.Noise
                || machineForAddLog.TempMax < model.Temp || machineForAddLog.TempMin > model.Temp
                || machineForAddLog.VibrationMax < model.Vibration || machineForAddLog.VibrationMin > model.Vibration)

            {
                if (machineForAddLog.Status == MachineStatus.Warning)
                    machineForAddLog.Status = MachineStatus.Critical;
                else
                    machineForAddLog.Status = MachineStatus.Warning;

                await _machineRepository.Update(machineForAddLog);
            }



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

    private string GetStatusForViewFromMachineStatusEnum(MachineStatus status)
    {
        switch (status)
        {
            case MachineStatus.Good:
                return "Bom";
            case MachineStatus.Warning:
                return "Cuidado";
            case MachineStatus.Critical:
                return "Critico";
            default:
                return "Indeterminado";
        }
    }

    public async Task<EditMachineViewModel> GetMachineDataForEdit(int machineId)
    {
        Machine machineForEditViewModel = await _machineRepository.GetById(machineId);

        EditMachineViewModel model = new EditMachineViewModel()
        {
            MachineId = machineForEditViewModel.Id,
            SerialNumber = machineForEditViewModel.SerialNumber,
            TempMax = machineForEditViewModel.TempMax,
            TempMin = machineForEditViewModel.TempMin,
            VibrationMax = machineForEditViewModel.VibrationMax,
            VibrationMin = machineForEditViewModel.VibrationMin,
            NoiseMax = machineForEditViewModel.NoiseMax,
            NoiseMin = machineForEditViewModel.NoiseMin,
            Brand = machineForEditViewModel.Brand,
            Category = machineForEditViewModel.Category.Name,
            Model = machineForEditViewModel.Model,
            Tag = machineForEditViewModel.Tag
        };

        return model;
    }

    public async Task<List<MachineForListViewModel>> GetMachinesByEnterpriseCnpj(string? enterpriseOfCurrentSessionCnpj)
    {
        try
        {
            IEnumerable<Machine> machinesInDb = await _machineRepository.GetAll();
            List<Machine> machinesOfCurrentEnterpriseSession = machinesInDb.Where(machine => machine.Enterprise.Cnpj == enterpriseOfCurrentSessionCnpj).ToList();

            List<MachineForListViewModel> machinesForView = new List<MachineForListViewModel>();

            if (machinesOfCurrentEnterpriseSession.Any())
            {
                foreach (Machine machine in machinesOfCurrentEnterpriseSession)
                {
                    Log logOfMachine = _logRepository.GetAll().Result
                   .Where(log => log.Machine.Id == machine.Id).MaxBy(log => log.Created_at);

                    machinesForView.Add(new MachineForListViewModel()
                    {
                        Id = machine.Id,
                        Brand = machine.Brand,
                        Model = machine.Model,
                        SerialNumber = machine.SerialNumber,
                        Status = GetStatusForViewFromMachineStatusEnum(machine.Status),
                        Noise = logOfMachine.Noise,
                        Temp = logOfMachine.Temp,
                        Vibration = logOfMachine.Vibration,
                        category = machine.Category.Name
                    });
                }

                return machinesForView;
            }

            return new List<MachineForListViewModel>();

        }
        catch
        {
            return new List<MachineForListViewModel>();
        }
    }

    public async Task<string> GetHistoryDataByCsvByMachineId(int machineId)
    {
        IEnumerable<Log> logsInDb = await _logRepository.GetAll();

        IEnumerable<Log> logsOfMachine = logsInDb.Where(log => log.Machine.Id == machineId);

        string csv = "";

        csv += "VIBRACAO;BARULHO;TEMPERATURA\n";

        foreach (Log log in logsOfMachine)
        {
            csv += log.Vibration.ToString() + ";" + log.Noise.ToString() + ";" + log.Temp.ToString() + "\n";
        }

        return csv;
    }

    public async Task<MachineDetailsViewModel> GetMachineForDetails(int id)
    {
        IEnumerable<Machine> machinesInDb = await _machineRepository.GetAll();

        Machine machineForExtractDataForViewModel = machinesInDb.FirstOrDefault(machine => machine.Id == id);
        IEnumerable<Log> logsOfMachines = await _logRepository.GetLogsWithMachines();

        Log lastLogOfMachine = logsOfMachines.Where(log => log.Machine.Id == id).OrderBy(log => log.Created_at).FirstOrDefault();

        if (machineForExtractDataForViewModel == null)
            return new MachineDetailsViewModel();

        MachineDetailsViewModel model = new MachineDetailsViewModel();

        model.Brand = machineForExtractDataForViewModel.Brand;
        model.SerialNumber = machineForExtractDataForViewModel.SerialNumber;
        model.Id = machineForExtractDataForViewModel.Id;
        model.Category = machineForExtractDataForViewModel.Category.Name;
        model.StatusName = ExtractStatusNameByMachineStatus(machineForExtractDataForViewModel.Status);
        model.Model = machineForExtractDataForViewModel.Model;
        model.RealTimeNoise = lastLogOfMachine.Noise;
        model.RealTimeTemp = lastLogOfMachine.Temp;
        model.RealTimeVibration = lastLogOfMachine.Vibration;

        return model;
    }

    private string ExtractStatusNameByMachineStatus(MachineStatus status)
    {
        switch (status)
        {
            case MachineStatus.Good:
                return "Bom";
            case MachineStatus.Warning:
                return "Atênção";
            case MachineStatus.Critical:
                return "Crítico";
            case MachineStatus.Undefined:
                return "Indefinido";
            default:
                return "Não identificado";
        }
    }

    public async Task<string> GetJsonForDetailsAboutMachineAjaxHandler(int id, string status)
    {
        try
        {
            IEnumerable<Machine> machinesInDb = await _machineRepository.GetAll();
            Machine machineForExtractData = machinesInDb.FirstOrDefault(machine => machine.Id == id);
            List<Log> logsOfMachineOrderByCreatedAt = _logRepository.GetAll().Result.OrderBy(log => log.Created_at).ToList();
            List<Log> lastSixLogsAboutMachine = new List<Log>();

            for (int i = 0; logsOfMachineOrderByCreatedAt.Count >= 6 ? i < 6 : i < logsOfMachineOrderByCreatedAt.Count; i++)
            {
                lastSixLogsAboutMachine.Add(logsOfMachineOrderByCreatedAt[i]);
            }

            Dictionary<int, double> hourWithValueOfMagnitude = new Dictionary<int, double>();

            foreach (Log log in lastSixLogsAboutMachine)
            {
                if (status == "temperatura")
                {
                    hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Temp);
                }
                else
                {
                    if (status == "vibracao")
                    {
                        hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Vibration);
                    }
                    else
                    {
                        hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Noise);
                    }
                }
            }

            string lastSixLogsAboutMachineAsJsonString = JsonConvert.SerializeObject(hourWithValueOfMagnitude).ToString();

            return lastSixLogsAboutMachineAsJsonString;
        }
        catch
        {
            return "";
        }



    }
}