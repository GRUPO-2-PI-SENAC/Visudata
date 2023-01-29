using PI.Domain.Entities;

namespace PI.Application.Intefaces;

public interface IUserSupportService
{
    Task<bool> CreateUserReport(UserSupport model);
}