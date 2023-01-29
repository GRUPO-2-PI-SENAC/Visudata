using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;

namespace PI.Application.Services;

public class UserSupportService : IUserSupportService
{
    private IUserSupportRepository _UserSupportRepository;
    private IEnterpriseRepository _EnterpriseRepository;
    private IUserProblemsCategoryRepository _UserProblemsCategoryRepository;

    public UserSupportService(IUserSupportRepository userSupportRepository, IEnterpriseRepository enterpriseRepository,
        IUserProblemsCategoryRepository userProblemsCategoryRepository)
    {
        _UserSupportRepository = userSupportRepository;
        _EnterpriseRepository = enterpriseRepository;
        _UserProblemsCategoryRepository = userProblemsCategoryRepository;
    }

    public async Task<bool> CreateUserReport(UserSupport model)
    {
        try
        {

            List<UserProblemsCategory> problemsCategories = (List<UserProblemsCategory>)await _UserProblemsCategoryRepository.GetAll();
            model.UserProblemsCategory = problemsCategories.First(problemCategory => problemCategory.Name == model.UserProblemsCategory.Name);
            model.Enterprise = await _EnterpriseRepository.GetById(model.Enterprise.Id);
            await _UserSupportRepository.Add(model);
            return true;
        }
        catch
        {
            return false;
        }
    }

}