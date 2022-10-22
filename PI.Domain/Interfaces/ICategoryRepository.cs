using PI.Domain.Entities;

namespace PI.Domain.Interfaces;

public interface IMachineCategoryRepository : IBaseRepository<MachineCategory>
{
    MachineCategory GetByName(string nameOfcategory);
}