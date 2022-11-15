using PI.Application.ViewModel.UserSupport;

namespace PI.Application.Intefaces;

public interface IUserSupportService
{
    Task<bool> CreateUserReport(AddUserSupportViewModel model);
}