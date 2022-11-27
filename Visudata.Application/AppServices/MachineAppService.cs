using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Machine;

namespace PI.Application.AppServices
{
    public class MachineAppService : IMachineAppService
    {
        private readonly IMachineService _machineService;

        public MachineAppService(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public Task<List<MachineForListViewModel>> GetAll(int enterpriseId)
        {
            return _machineService.GetAll(enterpriseId);
        }

        public Task<List<MachineForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId, string categoryName)
        {
            return _machineService.GetMachinesForSpecificCategory(enterpriseId, categoryName);
        }

        public Task<bool> Add(AddMachineViewModel model, string enterpriseCnpj)
        {
            return _machineService.Add(model, enterpriseCnpj);
        }

        public Task<bool> UpdateMachine(EditMachineViewModel model)
        {
            return _machineService.UpdateMachine(model);
        }

        public Task<bool> RemoveMachine(int machineId)
        {
            return _machineService.RemoveMachine(machineId);
        }

        public Task<List<MachineForListViewModel>> GetMachinesByEnterpriseId(int enterpriseId)
        {
            return _machineService.GetMachinesByEnterpriseId(enterpriseId);
        }

        public Task<JsonResult> GetStatusAboutTemp(int machineId, int enterpriseId)
        {
            return _machineService.GetStatusAboutTemp(machineId, enterpriseId);
        }

        public Task<JsonResult> GetStatusAboutVibration(int machineId, int enterpriseId)
        {
            return _machineService.GetStatusAboutVibration(machineId, enterpriseId);
        }

        public Task<JsonResult> GetStatusAboutNoise(int machineId, int enterpriseId)
        {
            return _machineService.GetStatusAboutNoise(machineId, enterpriseId);
        }

        public Task<AmountOfMachineByStatusViewModel> GetAmountOfMachinesByStatusWithEnterpriseId(int enterpriseId)
        {
            return _machineService.GetAmountOfMachinesByStatusWithEnterpriseId(enterpriseId);
        }

        public Task<bool> AddRegisterOfMachineFromJson(MachineDataRecieveFromSensorsJsonModel model)
        {
            return _machineService.AddRegisterOfMachineFromJson(model);
        }

        public Task<EditMachineViewModel> GetMachineDataForEdit(int machineId)
        {
            return _machineService.GetMachineDataForEdit(machineId);
        }

        public Task<List<MachineForListViewModel>> GetMachinesByEnterpriseCnpj(string? enterpriseOfCurrentSessionCnpj)
        {
            return _machineService.GetMachinesByEnterpriseCnpj(enterpriseOfCurrentSessionCnpj);
        }

        public Task<string> GetHistoryDataByCsvByMachineId(int machineId)
        {
            return _machineService.GetHistoryDataByCsvByMachineId(machineId);
        }

        public Task<MachineDetailsViewModel> GetMachineForDetails(int id)
        {
            return _machineService.GetMachineForDetails(id);
        }

        public Task<string> GetJsonForDetailsAboutMachineAjaxHandler(int id, string status)
        {
            return _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, status);
        }
    }
}
