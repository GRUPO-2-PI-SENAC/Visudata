namespace PI.Application.Intefaces
{
    public interface IUserProblemsCategoryAppService
    {
        Task<List<string>> GetNameOfAllAsString();

    }
}
