using PI.Domain.Entities;

namespace PI.Application.Intefaces;

public interface IEnterpriseService
{
    Task<Enterprise?> Login(string login, string password);
    Task<bool> SignUp(Enterprise model);
    Task<bool> Update(Enterprise model);
    Task<bool> DeleteByCnpj(string enterpriseCnpj);
    Task<Enterprise> GetByCnpj(string enterpriseCnpj);
}