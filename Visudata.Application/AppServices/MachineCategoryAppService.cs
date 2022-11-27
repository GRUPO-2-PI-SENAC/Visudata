using PI.Application.Intefaces;

namespace PI.Application.AppServices
{
    public class MachineCategoryAppService : IMachineCategoryAppService
    {
        private readonly IMachineCategoryService _machineCategoryService;

        public MachineCategoryAppService(IMachineCategoryService machineCategoryService)
        {
            _machineCategoryService = machineCategoryService;
        }

        public Task<List<string>> GetNameOfCategoriesAsString()
        {
            return _machineCategoryService.GetNameOfCategoriesAsString();
        }
    }
}
