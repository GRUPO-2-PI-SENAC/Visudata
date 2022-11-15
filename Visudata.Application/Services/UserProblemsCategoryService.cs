using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Application.Services
{
    public class UserProblemsCategoryService : IUserProblemsCategoryService
    {
        private readonly IUserProblemsCategoryRepository _userProblemsCategoryRepository;

        public UserProblemsCategoryService(IUserProblemsCategoryRepository userProblemsCategoryRepository)
        {
            _userProblemsCategoryRepository = userProblemsCategoryRepository;
        }

        public async Task<List<string>> GetNameOfAllAsString()
        {
            List<Domain.Entities.UserProblemsCategory> userProblemsCategoryAsList = _userProblemsCategoryRepository.GetAll().Result.ToList();

            List<UserProblemsCategory> userProblemsCategories = _userProblemsCategoryRepository.GetAll().Result.ToList();

            List<string> nameOfUserSupportCategory = new List<string>();

            foreach (UserProblemsCategory userProblemsCategory in userProblemsCategories)
            {
                nameOfUserSupportCategory.Add(userProblemsCategory.Name);
            }
            return nameOfUserSupportCategory;
        }
    }
}
