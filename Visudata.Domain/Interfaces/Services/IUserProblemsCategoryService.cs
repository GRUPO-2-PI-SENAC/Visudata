namespace PI.Application.Intefaces
{
    public interface IUserProblemsCategoryService 
    {
        Task<List<string>> GetNameOfAllAsString();

    }
}
