using PI.Domain.Entities;

namespace PI.Domain.Interfaces;

public interface IUserProblemsCategoryRepository : IBaseRepository<UserProblemsCategory>
{
    Task<UserProblemsCategory> GetByName(string nameOfProblemCategory);
}