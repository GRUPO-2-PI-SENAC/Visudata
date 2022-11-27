using PI.Application.Intefaces;
using PI.Domain.ViewModel.Enterpriqse;
using PI.Domain.ViewModel.Enterprise;

namespace PI.Application.AppServices
{
    public class EnterpriseAppService : IEnterpriseAppService
    {
        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseAppService(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        public Task<bool> Login(EnterpriseLoginViewModel model)
        {
            return _enterpriseService.Login(model);
        }

        public Task<bool> SignUp(CreateEnterpriseViewModel model)
        {
            return _enterpriseService.SignUp(model);
        }

        public Task<bool> Update(UpdateEnterpriseViewModel model)
        {
            return _enterpriseService.Update(model);
        }

        public Task<bool> Remove(int enterpriseId)
        {
            return _enterpriseService.Remove(enterpriseId);
        }

        public Task<EnterpriseProfileViewModel> GetEnterpriseForProfileById(int enterpriseId)
        {
            return _enterpriseService.GetEnterpriseForProfileById(enterpriseId);
        }

        public Task<EnterpriseProfileViewModel> GetEnterpriseByCnpj(string enterpriseCnpj)
        {
            return _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj);
        }

        public Task<AmountOfMachinesStatusByEnterpriseViewModel> GetMachinesStatusByEnterpriseCnpj(string enterpriseCnpj)
        {
            return _enterpriseService.GetMachinesStatusByEnterpriseCnpj(enterpriseCnpj);
        }
    }
}
