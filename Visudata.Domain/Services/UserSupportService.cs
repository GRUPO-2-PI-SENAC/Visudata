using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Domain.ViewModel.UserSupport;

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

    public async Task<bool> CreateUserReport(AddUserSupportViewModel model)
    {
        try
        {
            IEnumerable<UserSupport> userSupports = await _UserSupportRepository.GetAll();

            bool isAlreadTheSameProblemInTheDatabase = userSupports.Any(userSupport =>
                userSupport.Description == model.ProblemDescription &&
                userSupport.AddressEmailOfRepresentativeEmployee ==
                model.RepresentativeEmailAddress);

            if (isAlreadTheSameProblemInTheDatabase)
                return false;

            Enterprise? enterpriseWhichCreateProblemReport = await _EnterpriseRepository.GetById(model.EnterpriseId);

            if (enterpriseWhichCreateProblemReport.Equals(null))
                return false;

            UserSupport userSupportForAddInDb = new UserSupport()
            {
                Description = model.ProblemDescription,
                AddressEmailOfRepresentativeEmployee = model.RepresentativeEmailAddress,
                Created_at = model.Created_at,
                Enterprise = enterpriseWhichCreateProblemReport,
                UserProblemsCategory = await _UserProblemsCategoryRepository.GetByName(model.ProblemsCategoryName)
            };
            await _UserSupportRepository.Add(userSupportForAddInDb);
            return true;
        }
        catch
        {
            return false;
        }
    }

}