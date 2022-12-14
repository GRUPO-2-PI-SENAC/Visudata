using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.ViewModel.Machine;
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

        public Task<GraphicModel> GetJsonForDetailsAboutMachineAjaxHandler(int id, string status)
        {
            return _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, status);
        }

        public Task<List<MachineForListViewModel>> GetMachineOfSpecificCategory(string? currentSessionEnterpriseCnpj,
            string nameOfcategory)
        {
            return _machineService.GetMachinesOfSpecificCategory(currentSessionEnterpriseCnpj, nameOfcategory);
        }

        public Task<List<Machine>> GetAllMachineEntity(int enterpriseId)
        {
            return _machineService.GetAllMachineEntity(enterpriseId);
        }

        public Task<List<MachineForListModelAPI>> GetMachineList(int enterpriseId)
        {
            return _machineService.GetMachineList(enterpriseId);
        }

        public Task<List<MachineForAPIListViewModel>> GetMachinesForApiList(string enterpriseCnpj)
        {
            return _machineService.GetMachinesForApiList(enterpriseCnpj);
        }

        public Task<Machine> GetMachineEntityById(int machineId)
        {
            return _machineService.GetMachineEntityById(machineId);
        }
        public Task<EditMachineModel> GetEditMachineModel(int machineId)
        {
            return _machineService.GetEditMachineModel(machineId);
        }

        public Task<List<RegisterMachineLogsViewModel>> GetRegisterAboutMachine(int id)
        {
            return _machineService.GetRegisterAboutMachines(id);
        }

        public Task<List<MachineForListViewModel>> GetMachineForStatus(string? enterpriseOfCurrentSessionCnpj, string status)
        {
            return _machineService.GetMachineForStatus(enterpriseOfCurrentSessionCnpj, status);
        }
    }
}
