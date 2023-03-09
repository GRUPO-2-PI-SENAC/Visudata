using PI.Application.Intefaces;
using PI.Domain.Entities;

namespace PI.Application.AppServices
{
    public class EnterpriseAppService : IEnterpriseAppService
    {
        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseAppService(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        public Task<Enterprise> GetByCnpj(string enterpriseCnpj)
        {
            return _enterpriseService.GetByCnpj(enterpriseCnpj);
        }

        public Task<Enterprise?> Login(string login, string password)
        {
            return _enterpriseService.Login(login, password);
        }

        public Task<bool> Remove(string enterpriseCnpj)
        {
            return _enterpriseService.DeleteByCnpj(enterpriseCnpj);
        }

        public Task<bool> SignUp(Enterprise model)
        {
            return _enterpriseService.SignUp(model);
        }

        public Task<bool> Update(Enterprise model)
        {
            return _enterpriseService.Update(model);
        }
    }
}
