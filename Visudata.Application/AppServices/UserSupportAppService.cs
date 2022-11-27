using PI.Application.Intefaces;
using PI.Application.ViewModel.UserSupport;

namespace PI.Application.AppServices
{
    public class UserSupportAppService : IUserSupportAppService
    {
        private readonly IUserSupportService _userSupportService;

        public UserSupportAppService(IUserSupportService userSupportService)
        {
            _userSupportService = userSupportService;
        }

        public Task<bool> CreateUserReport(AddUserSupportViewModel model)
        {
            return _userSupportService.CreateUserReport(model);
        }
    }
}
