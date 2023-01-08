using FakeItEasy;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using System.Runtime.CompilerServices;
using System.Web.Http.ModelBinding;
using PI.Domain.ViewModel.Enterprise;
using PI.Domain.ViewModel.UserSupport;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Brazil;

namespace Visudata.Test.Controllers
{
    public class EnterpriseControllerTest
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly IUserSupportService _userSupportService;
        private readonly IUserProblemsCategoryService _userProblemsCategoryService;
        private readonly Faker _fake;

        public EnterpriseControllerTest()
        {
            _enterpriseService = A.Fake<IEnterpriseService>();
            _userSupportService = A.Fake<IUserSupportService>();
            _userProblemsCategoryService = A.Fake<IUserProblemsCategoryService>();
            _fake = new Faker();

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
            string enterpriseCnpj = _fake.Company.Cnpj();
            EnterpriseProfileViewModel enterpriseModel = A.Fake<EnterpriseProfileViewModel>();
            A.CallTo(() => _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj)).Returns(enterpriseModel);
            model.EnterpriseId = enterpriseModel.Id;
            bool isUpdated = _fake.Random.Bool();
            A.CallTo(() => _userSupportService.CreateUserReport(model)).Returns(isUpdated);
            var result = await _userSupportService.CreateUserReport(model);

            result.Should().BeTrue();



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
