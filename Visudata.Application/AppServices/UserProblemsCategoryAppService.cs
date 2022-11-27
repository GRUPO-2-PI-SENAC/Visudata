using PI.Application.Intefaces;

namespace PI.Application.AppServices
{
    public class UserProblemsCategoryAppService : IUserProblemsCategoryAppService
    {
        private readonly IUserProblemsCategoryService _userProblemsCategoryService;

        public UserProblemsCategoryAppService(IUserProblemsCategoryService userProblemsCategoryService)
        {
            _userProblemsCategoryService = userProblemsCategoryService;
        }

        public Task<List<string>> GetNameOfAllAsString()
        {
            return _userProblemsCategoryService.GetNameOfAllAsString();
        }
    }
}
