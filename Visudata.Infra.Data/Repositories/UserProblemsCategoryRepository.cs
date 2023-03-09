using Dapper;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using PI.Infra.Data.Context;

namespace PI.Infra.Data.Repositories;

public class UserProblemsCategoryRepository : BaseRepository<UserProblemsCategory>, IUserProblemsCategoryRepository
{
    public UserProblemsCategoryRepository(VisudataDbContext visudataDbContext) : base(visudataDbContext)
    {
    }

    public async Task<UserProblemsCategory> GetByName(string nameOfProblemCategory)
    {
        return await Task.Run(async () =>
        {
            var query = @"select * from us_problems_category where Name = @name;";
            UserProblemsCategory userProblemByName = (await _databaseConnection.QueryAsync<UserProblemsCategory>(query, new { name = nameOfProblemCategory })).First();
            return userProblemByName;
        });
    }
}