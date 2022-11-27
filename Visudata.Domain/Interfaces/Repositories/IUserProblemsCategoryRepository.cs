using PI.Domain.Entities;

namespace PI.Domain.Interfaces.Repositories;

public interface IUserProblemsCategoryRepository : IBaseRepository<UserProblemsCategory>
{
    Task<UserProblemsCategory> GetByName(string nameOfProblemCategory);
}