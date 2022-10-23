using PI.Application.Intefaces;
using PI.Application.ViewModel.Enterprise;
using PI.Domain.Entities;
using PI.Domain.Interfaces;

namespace PI.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository;
    private readonly IEnterpriseStatusRepository _enterpriseStatusRepository;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository, IEnterpriseStatusRepository enterpriseStatusRepository)
    {
        _enterpriseRepository = enterpriseRepository;
        _enterpriseStatusRepository = enterpriseStatusRepository;
    }

    public async Task<bool> Login(EnterpriseLoginViewModel model)
    {
        IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();

        bool isInDb = enterprises.Any(enterprise => enterprise.Cnpj == model.Login && enterprise.Password == model.Password && enterprise.EnterpriseStatus.Name == "ACTIVE");

        return isInDb;
    }

    public async Task<bool> Remove(int enterpriseId)
    {
        Enterprise? enter = await _enterpriseRepository.GetById(enterpriseId);

        if (enter != null)
        {
            try
            {
                await _enterpriseRepository.RemoveById(enter.Id);
                return true;
            }
            catch
            {
                return false; 
            }

        }
        return false;
    }

    public async Task<bool> SignUp(CreateEnterpriseViewModel model)
    {
        if (model.Password != model.ConfirmPassword) return false;

        IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();

        bool existInDb = enterprises.Any(enterprise => model.CNPJ == enterprise.Cnpj);

        if (existInDb) return false;

        try
        {

            Enterprise enterpriseForAddInDb = new Enterprise()
            {
                Id = model.Id,
                FantasyName = model.FantasyName,
                SocialReason = model.SocialReason,
                Address = model.Address,
                Password = model.Password,
                City = model.City,
                NumberOfLocation = model.NumberOfLocation,
                Sector = model.Sector,
                Cnpj = model.CNPJ,
                EnterpriseStatus = _enterpriseStatusRepository.GetAll().Result.FirstOrDefault(status => status.Name == "ACTIVE"),
                State = model.State,
                Created_at = DateTime.Now
            };

            await _enterpriseRepository.Add(enterpriseForAddInDb);

            return true;
        }
        catch
        {
            return false;
        }


    }

    public async Task<bool> Update(UpdateEnterpriseViewModel model)
    {

        Enterprise? enterpriseForUpdate = await _enterpriseRepository.GetById(model.Id);

        if (enterpriseForUpdate != null)
        {
            if (model.Password == model.ConfirmPassword)
            {
                enterpriseForUpdate.Password = model.Password;

                await _enterpriseRepository.Update(enterpriseForUpdate);

                return true;

            }

            return false;
        }

        return false;
    }
}