using PI.Application.Intefaces;
using PI.Application.ViewModel.Enterprise;
using PI.Domain.Entities;
using PI.Domain.Interfaces;

namespace PI.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IEnterpriseStatusRepository _enterpriseStatusRepository;
    private readonly ILogsRepository _logsRepository;
    private readonly IOutlierRegisterRepository _outlierRegisterRepository;
    private readonly IMachineService _machineService;
    private readonly IMachineRepository _machineRepository;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository, IEnterpriseStatusRepository enterpriseStatusRepository, ILogsRepository logsRepository, IOutlierRegisterRepository outlierRegisterRepository, IMachineService machineService, IMachineRepository machineRepository)
    {
        _enterpriseRepository = enterpriseRepository;
        _enterpriseStatusRepository = enterpriseStatusRepository;
        _logsRepository = logsRepository;
        _outlierRegisterRepository = outlierRegisterRepository;
        _machineService = machineService;
        _machineRepository = machineRepository;
    }

    public async Task<EnterpriseProfileViewModel> GetEnterpriseByCnpj(string enterpriseCnpj)
    {
        try
        {
            List<Enterprise> enterprisesInDb = _enterpriseRepository.GetAll().Result.ToList();

            Enterprise enterpriseFromCnpj = enterprisesInDb.FirstOrDefault(enterprise => enterprise.Cnpj == enterpriseCnpj);

            if (enterpriseFromCnpj == null)
                return new EnterpriseProfileViewModel();

            return new EnterpriseProfileViewModel()
            {
                Id = enterpriseFromCnpj.Id,
                Address = enterpriseFromCnpj.Address,
                City = enterpriseFromCnpj.City,
                FantasyName = enterpriseFromCnpj.FantasyName,
                Sector = enterpriseFromCnpj.Sector,
                State = enterpriseFromCnpj.State
            };
        }
        catch
        {
            return new EnterpriseProfileViewModel();
        }

    }

    public async Task<EnterpriseProfileViewModel> GetEnterpriseForProfileById(int enterpriseId)
    {
        try
        {
            List<Enterprise> enterprises = _enterpriseRepository.GetAll().Result.ToList();

            Enterprise? enterpriseForView = await _enterpriseRepository.GetById(enterpriseId);

            if (enterpriseForView == null)
                return null;

            EnterpriseProfileViewModel enterpriseViewModelForProfile = new EnterpriseProfileViewModel
            {
                Id = enterpriseForView.Id,
                FantasyName = enterpriseForView.FantasyName,
                City = enterpriseForView.City,
                State = enterpriseForView.State,
                Sector = enterpriseForView.Sector,
                Address = enterpriseForView.Address
            };

            return enterpriseViewModelForProfile;

        }
        catch
        {
            return null;
        }

    }

    public async Task<AmountOfMachinesStatusByEnterpriseViewModel> GetMachinesStatusByEnterpriseCnpj(string enterpriseCnpj)
    {
        AmountOfMachinesStatusByEnterpriseViewModel model = new AmountOfMachinesStatusByEnterpriseViewModel();

        // for initialize the property in object for incremented later in for loop 
        model.AmountOfCriticalStateMachines = 0;

        List<Machine> machinesOfEnterprise = await _machineRepository.GetMachinesByEnterpriseCnpj(enterpriseCnpj);
        IEnumerable<OutlierRegister> outlierRegistersInDb = await _outlierRegisterRepository.GetAll();
        IEnumerable<Log> LogsInDb = await _logsRepository.GetAll();
        List<Log> logsOfEnterprise = LogsInDb.Where(log => log.Machine.Enterprise.Cnpj == enterpriseCnpj).Where(log => log.Created_at.Hour >= DateTime.Now.Hour - 6).ToList();
        List<OutlierRegister> outlierRegisterOfEnterpriseRelatedAtLog = new List<OutlierRegister>();

        List<Log> logsOfGoodMachines = new List<Log>();
        List<Log> logsOfWarningsMachines = new List<Log>();
        List<Log> logsOfCriticalMachines = new List<Log>();

        //For identify logs of warning or good machines
        foreach (Log log in logsOfEnterprise)
        {
            int amountOfRegister = outlierRegisterOfEnterpriseRelatedAtLog.Count;

            foreach (OutlierRegister register in outlierRegistersInDb)
            {
                if (register.Log.Id == log.Id)
                {
                    outlierRegisterOfEnterpriseRelatedAtLog.Add(register);
                }
            }
            if (amountOfRegister == outlierRegisterOfEnterpriseRelatedAtLog.Count)
            {
                Machine machine = log.Machine;

                if (machine.NoiseMax < log.Noise || machine.NoiseMin > log.Noise)
                {
                    logsOfWarningsMachines.Add(log);
                }
                else
                {
                    if (machine.TempMax < log.Temp || machine.TempMim > log.Temp)
                    {
                        logsOfWarningsMachines.Add(log);
                    }
                    else
                    {
                        if (machine.VibrationMax < log.Vibration || machine.VibrationMin > log.Temp)
                        {
                            logsOfWarningsMachines.Add(log);
                        }
                        else
                        {
                            logsOfGoodMachines.Add(log);
                        }
                    }
                }
            }
        }

        foreach (Machine machine in machinesOfEnterprise)
        {
            int amountOfMachinePresenceInOutlierRegister = 0;

            foreach (OutlierRegister register in outlierRegisterOfEnterpriseRelatedAtLog)
            {
                if (register.Log.Machine.Id == machine.Id)
                    amountOfMachinePresenceInOutlierRegister++;
            }

            if (amountOfMachinePresenceInOutlierRegister > 1)
            {
                model.AmountOfCriticalStateMachines++;
                logsOfWarningsMachines.RemoveAll(log => log.Machine.Id == machine.Id);
            }
        }

        model.AmountOfGoodStateMachines = logsOfGoodMachines.Count;
        model.AmountOfWarningStateMachines = logsOfWarningsMachines.Count;
        model.AmountOfMachines = machinesOfEnterprise.Count;

        return model;
    }

    public async Task<bool> Login(EnterpriseLoginViewModel model)
    {
        IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();

        bool isInDb = enterprises.Any(enterprise => enterprise.Cnpj == model.Login && enterprise.Password == model.Password && enterprise.EnterpriseStatus.Name == "ACTIVE");

        return isInDb;
    }

    public async Task<bool> Remove(int enterpriseId)
    {
        Enterprise? enterpriseForRemove = await _enterpriseRepository.GetById(enterpriseId);

        if (enterpriseForRemove != null)
        {
            try
            {
                await _enterpriseRepository.RemoveById(enterpriseForRemove.Id);
                return true;
            }
            catch
            {
                return false;
            }

        }
        return false;
    }

    public async Task<bool> SignUp(CreateEnterpriseViewModel model)
    {
        if (model.Password != model.ConfirmPassword) return false;

        IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();

        bool existInDb = enterprises.Any(enterprise => model.CNPJ == enterprise.Cnpj);

        if (existInDb) return false;

        try
        {

            Enterprise enterpriseForAddInDb = new Enterprise()
            {
                Id = model.Id,
                FantasyName = model.FantasyName,
                SocialReason = model.SocialReason,
                Address = model.Address,
                Password = model.Password,
                City = model.City,
                NumberOfLocation = model.NumberOfLocation,
                Sector = model.Sector,
                Cnpj = model.CNPJ,
                EnterpriseStatus = _enterpriseStatusRepository.GetAll().Result.FirstOrDefault(status => status.Name == "ACTIVE"),
                State = model.State,
                Created_at = DateTime.Now
            };

            await _enterpriseRepository.Add(enterpriseForAddInDb);

            return true;
        }
        catch
        {
            return false;
        }


    }

    public async Task<bool> Update(UpdateEnterpriseViewModel model)
    {

        Enterprise? enterpriseForUpdate = await _enterpriseRepository.GetById(model.Id);

        if (enterpriseForUpdate != null)
        {
            if (model.Password == model.ConfirmPassword)
            {
                enterpriseForUpdate.Password = model.Password;

                await _enterpriseRepository.Update(enterpriseForUpdate);

                return true;
            }

            return false;
        }
        return false;
    }
}