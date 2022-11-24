using FakeItEasy;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Enterprise;
using PI.Application.ViewModel.UserSupport;
using PI.Domain.Entities;
using System.Runtime.CompilerServices;
using System.Web.Http.ModelBinding;

namespace Visudata.Test.Controllers
{
    public class EnterpriseControllerTest
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly IUserSupportService _userSupportService;
        private readonly IUserProblemsCategoryService _userProblemsCategoryService;
        
        public EnterpriseControllerTest()
        {
            _enterpriseService = A.Fake<IEnterpriseService>();
            _userSupportService = A.Fake<IUserSupportService>();
            _userProblemsCategoryService = A.Fake<IUserProblemsCategoryService>();
        }

        [Fact]
        public async void Post_Login()
        {
            EnterpriseLoginViewModel model = A.Fake<EnterpriseLoginViewModel>();

            A.CallTo(() => _enterpriseService.Login(model)).Returns(true);

            bool isUpdated = await _enterpriseService.Login(model);

            Assert.True(isUpdated);
        }
        [Fact]
        public async Task Post_LoginIfLoginIsInvalidAsync()
        {
            EnterpriseLoginViewModel model = A.Fake<EnterpriseLoginViewModel>();
            model.Login = null;

            A.CallTo(() => _enterpriseService.Login(model)).Returns(false);

            bool isUpdated = await _enterpriseService.Login(model);

            Assert.False(isUpdated);
        }

        [Fact]
        public async Task Post_SupportWorks()
        {
            AddUserSupportViewModel model = A.Fake<AddUserSupportViewModel>();
            bool isAdded = await _userSupportService.CreateUserReport(model);
            Assert.True(isAdded);
        }

        [Fact]
        public async Task Post_SupportDontWorks()
        {
            AddUserSupportViewModel model = A.Fake<AddUserSupportViewModel>();
            model.EnterpriseId = 0;
            Assert.False((await _userSupportService.CreateUserReport(model)));
        }
    }
}
