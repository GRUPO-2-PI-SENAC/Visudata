using Microsoft.AspNetCore.Mvc;
using PI.Application.ViewModel.Machine;

namespace PI.Application.Intefaces;

public interface IMachineService
{
    #region CRUD

    Task<List<MachinesForListViewModel>> GetAll(int enterpriseId);
    Task<List<MachinesForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId, string categoryName);
    Task<bool> Add(AddMachineViewModel model, string enterpriseCnpj);
    Task<bool> UpdateMachine(EditMachineViewModel model);
    Task<bool> RemoveMachine(int machineId);
    Task<List<MachinesForListViewModel>> GetMachinesByEnterpriseId(int enterpriseId);

    #endregion

    #region MachineStatus

    Task<JsonResult> GetStatusAboutTemp(int machineId, int enterpriseId);
    Task<JsonResult> GetStatusAboutVibration(int machineId, int enterpriseId);
    Task<JsonResult> GetStatusAboutNoise(int machineId, int enterpriseId);
    Task<AmountOfMachineByStatusViewModel> GetAmountOfMachinesByStatusWithEnterpriseId(int enterpriseId);
    Task<bool> AddRegisterOfMachineFromJson(MachineDataRecieveFromSensorsJsonModel model);
    Task<EditMachineViewModel> GetMachineDataForEdit(int machineId);

    #endregion

}