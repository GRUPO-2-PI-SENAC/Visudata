using PI.Domain.Entities;

namespace PI.Application.Intefaces
{
    public interface IEnterpriseAppService
    {
        Task<Enterprise?> Login(string login, string password);
        Task<bool> SignUp(Enterprise model);
        Task<bool> Update(Enterprise model);
        Task<bool> Remove(string enterpriseCnpj);
        Task<Enterprise> GetByCnpj(string enterpriseCnpj);
    }
}
