using Microsoft.AspNetCore.Mvc;
using PI.Application.ViewModel.Machine;

namespace PI.Application.Intefaces;

public interface IMachineService
{
    Task<List<MachinesForListViewModel>> GetAll(int enterpriseId);
    Task<List<MachinesForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId, string categoryName);
    Task<bool> CreateNewMachine(AddMachineViewModel model, int enterpriseId);
    Task<bool> UpdateMachine(int MachineId, AddMachineViewModel model);
    Task<bool> RemoveMachine(int machineId);
    Task<bool> UpdateMachineStatus(int machineId, int enterpriseId,int statusId);
    Task<JsonResult> GetStatusAboutTemp(int machineId, int enterpriseId);
    Task<JsonResult> GetStatusAboutVibration(int machineId, int enterpriseId);
    Task<JsonResult> GetStatusAboutNoise(int machineId, int enterpriseId);
}