using PI.Application.ViewModel.Machine;

namespace PI.Application.Intefaces;

public interface IMachineService
{
    Task<List<MachinesForListViewModel>> GetAll(int enterpriseId);
    Task<List<MachinesForListViewModel>> GetMachinesForSpecificCategory(int enterpriseId, string categoryName);
    Task<bool> CreateNewMachine(AddMachineViewModel model);
}