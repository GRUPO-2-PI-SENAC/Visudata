using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class CategoryRepository : BaseRepository<MachineCategory>, IMachineCategoryRepository
{
    public CategoryRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public MachineCategory GetByName(string nameOfcategory)
    {
        MachineCategory? firstOrDefault =  GetAll().Result.FirstOrDefault(category => category.Name == nameOfcategory);

        return firstOrDefault;
    }
}