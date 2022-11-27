using PI.Domain.ViewModel.Enterpriqse;
using PI.Domain.ViewModel.Enterprise;

namespace PI.Application.Intefaces
{
    public interface IEnterpriseAppService
    {
        Task<bool> Login(EnterpriseLoginViewModel model);
        Task<bool> SignUp(CreateEnterpriseViewModel model);
        Task<bool> Update(UpdateEnterpriseViewModel model);
        Task<bool> Remove(int enterpriseId);
        Task<EnterpriseProfileViewModel> GetEnterpriseForProfileById(int enterpriseId);
        Task<EnterpriseProfileViewModel> GetEnterpriseByCnpj(string enterpriseCnpj);
        Task<AmountOfMachinesStatusByEnterpriseViewModel> GetMachinesStatusByEnterpriseCnpj(string enterpriseCnpj);
    }
}
