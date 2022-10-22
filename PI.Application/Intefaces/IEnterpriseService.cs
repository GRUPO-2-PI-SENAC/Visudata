using PI.Application.ViewModel.Enterprise;

namespace PI.Application.Intefaces;

public interface IEnterpriseService
{
    Task<bool> Login(EnterpriseLoginViewModel model);
    Task<bool> SignUp(CreateEnterpriseViewModel model);
}