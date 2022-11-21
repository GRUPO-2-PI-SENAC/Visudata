namespace PI.Application.Intefaces
{
    public interface IMachineCategoryService
    {
        Task<List<string>> GetNameOfCategoriesAsString();
    }
}
