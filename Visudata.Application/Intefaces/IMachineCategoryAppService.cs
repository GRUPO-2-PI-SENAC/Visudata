namespace PI.Application.Intefaces
{
    public interface IMachineCategoryAppService
    {
        Task<List<string>> GetNameOfCategoriesAsString();
    }
}
