using PI.Domain.Entities;

namespace PI.Application.Intefaces
{
    public interface IUserSupportAppService
    {
        Task<bool> CreateUserReport(UserSupport model);
    }
}
