using PI.Application.ViewModel.UserSupport;

namespace PI.Application.Intefaces
{
    public interface IUserSupportAppService
    {
        Task<bool> CreateUserReport(AddUserSupportViewModel model);
    }
}
