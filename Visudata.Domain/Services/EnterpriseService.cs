using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Domain.Util;
using System.Reflection;

namespace PI.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;

    }

    public async Task<bool> DeleteByCnpj(string enterpriseCnpj)
    {
        try
        {
            Enterprise forDelete = await _enterpriseRepository.GetEnterpriseByCnpj(enterpriseCnpj);

            await _enterpriseRepository.Delete(forDelete.Id);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Enterprise> GetByCnpj(string enterpriseCnpj)
    {
        try
        {
            return await _enterpriseRepository.GetEnterpriseByCnpj(enterpriseCnpj);
        }
        catch
        {
            return new Enterprise();
        }

    }

    //public async Task<EnterpriseProfileViewModel> GetEnterpriseByCnpj(string enterpriseCnpj)
    //{
    //    try
    //    {
    //        List<Enterprise> enterprisesInDb = _enterpriseRepository.GetAll().Result.ToList();

    //        Enterprise enterpriseFromCnpj = enterprisesInDb.FirstOrDefault(enterprise => enterprise.Cnpj == enterpriseCnpj);

    //        if (enterpriseFromCnpj == null)
    //            return new EnterpriseProfileViewModel();

    //        return new EnterpriseProfileViewModel()
    //        {
    //            Id = enterpriseFromCnpj.Id,
    //            Address = enterpriseFromCnpj.Address,
    //            City = enterpriseFromCnpj.City,
    //            FantasyName = enterpriseFromCnpj.FantasyName,
    //            Sector = enterpriseFromCnpj.Sector,
    //            State = enterpriseFromCnpj.State
    //        };
    //    }
    //    catch
    //    {
    //        return new EnterpriseProfileViewModel();
    //    }

    //}

    //public async Task<EnterpriseProfileViewModel> GetEnterpriseForProfileById(int enterpriseId)
    //{
    //    try
    //    {
    //        List<Enterprise> enterprises = _enterpriseRepository.GetAll().Result.ToList();

    //        Enterprise? enterpriseForView = await _enterpriseRepository.GetById(enterpriseId);

    //        if (enterpriseForView == null)
    //            return null;

    //        EnterpriseProfileViewModel enterpriseViewModelForProfile = new EnterpriseProfileViewModel
    //        {
    //            Id = enterpriseForView.Id,
    //            FantasyName = enterpriseForView.FantasyName,
    //            City = enterpriseForView.City,
    //            State = enterpriseForView.State,
    //            Sector = enterpriseForView.Sector,
    //            Address = enterpriseForView.Address
    //        };

    //        return enterpriseViewModelForProfile;

    //    }
    //    catch
    //    {
    //        return null;
    //    }

    //}

    public async Task<Enterprise?> Login(string login, string password)
    {
        try
        {
            IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();
            Enterprise currentEnterprise = enterprises.First(enterprise => enterprise.Cnpj == login && Encrypt.DecryptPassword(enterprise.Password) == password && enterprise.Status == Enterprise_Status.ACTIVE);
            return currentEnterprise;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> Remove(int enterpriseId)
    {
        Enterprise? enterpriseForRemove = await _enterpriseRepository.GetById(enterpriseId);

        if (enterpriseForRemove != null)
        {
            try
            {
                await _enterpriseRepository.Delete(enterpriseForRemove.Id);
                return true;
            }
            catch
            {
                return false;
            }

        }
        return false;
    }

    //public async Task<bool> SignUp(CreateEnterpriseViewModel model)
    //{
    //    if (model.Password != model.ConfirmPassword) return false;

    //    IEnumerable<Enterprise> enterprises = await _enterpriseRepository.GetAll();

    //    bool existInDb = enterprises.Any(enterprise => model.CNPJ == enterprise.Cnpj);

    //    if (existInDb) return false;

    //    try
    //    {
    //        string passwordEncrypted = Encrypt.EncryptPassword(model.ConfirmPassword);

    //        Enterprise enterpriseForAddInDb = new Enterprise()
    //        {
    //            Id = model.Id,
    //            FantasyName = model.FantasyName,
    //            SocialReason = model.SocialReason,
    //            Address = model.Address,
    //            Password = passwordEncrypted,
    //            City = model.City,
    //            NumberOfLocation = model.NumberOfLocation,
    //            Sector = model.Sector,
    //            Cnpj = model.CNPJ,
    //            Status = Enterprise_Status.ACTIVE,
    //            State = model.State,
    //            Created_at = DateTime.Now
    //        };

    //        await _enterpriseRepository.Add(enterpriseForAddInDb);

    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }


    //}

    public async Task<bool> SignUp(Enterprise model)
    {
        try
        {
            await _enterpriseRepository.Add(model);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> Update(Enterprise model)
    {
        try
        {
            Enterprise enterpriseForUpdate = await _enterpriseRepository.GetById(model.Id);
            enterpriseForUpdate = model;

            await _enterpriseRepository.Update(enterpriseForUpdate);

            return true;
        }
        catch
        {
            return false;
        }
    }
}