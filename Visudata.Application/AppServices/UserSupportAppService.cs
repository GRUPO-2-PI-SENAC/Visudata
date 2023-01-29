using PI.Application.Intefaces;
using PI.Domain.Entities;

namespace PI.Application.AppServices
{
    public class UserSupportAppService : IUserSupportAppService
    {
        private readonly IUserSupportService _userSupportService;

        public UserSupportAppService(IUserSupportService userSupportService)
        {
            _userSupportService = userSupportService;
        }

        public Task<bool> CreateUserReport(UserSupport model)
        {
            return _userSupportService.CreateUserReport(model);
        }
    }
}
