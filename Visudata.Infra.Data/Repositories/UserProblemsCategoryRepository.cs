using PI.Domain.Entities;
using PI.Domain.Interfaces;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class UserProblemsCategoryRepository : BaseRepository<UserProblemsCategory>, IUserProblemsCategoryRepository 
{
    public UserProblemsCategoryRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<UserProblemsCategory> GetByName(string nameOfProblemCategory)
    {
        List<UserProblemsCategory> userProblemsCategoriesInDb =  _context.UserProblemsCategories.ToList();

        UserProblemsCategory? problemsCategoryFromName = userProblemsCategoriesInDb.FirstOrDefault(userProblemsCategory => userProblemsCategory.Name == nameOfProblemCategory);

        return problemsCategoryFromName.Equals(null) ? new UserProblemsCategory() : problemsCategoryFromName;
    }
}