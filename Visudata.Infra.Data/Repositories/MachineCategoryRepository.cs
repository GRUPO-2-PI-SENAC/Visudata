using Microsoft.EntityFrameworkCore;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories
{
    public class MachineCategoryRepository : BaseRepository<MachineCategory>, IMachineCategoryRepository
    {
        public MachineCategoryRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
        {
        }

        public async Task<IEnumerable<MachineCategory>?> GetAllByEnterprise(int enterpriseId)
        {
            List<MachineCategory> machineCategories = _context.MachineCategories.Include(mach => mach.Enterprise).ToList();

            List<MachineCategory>? machineCategoriesByEnterpriseId = machineCategories.Where(mc => mc.Enterprise.Id == enterpriseId).ToList();

            return machineCategoriesByEnterpriseId == null ? machineCategoriesByEnterpriseId : new List<MachineCategory>();
        }

        public async Task<MachineCategory?> GetByName(string name)
        {
            MachineCategory? machineCategoryByName = _context.MachineCategories.FirstOrDefault(machineCategories => machineCategories.Name == name);

            return machineCategoryByName == null ? machineCategoryByName : new MachineCategory();
        }

        public async Task<IEnumerable<Machine>> GetMachinesOfCategory(int categoryId)
        {
            List<Machine> machines = _context.Machines.Include(machine => machine.Category).ToList();
            IEnumerable<Machine>? machinesOfspecificyCategoryById = machines.Where(machine => machine.Category.Id == categoryId);
            return machinesOfspecificyCategoryById;
        }
    }
}
