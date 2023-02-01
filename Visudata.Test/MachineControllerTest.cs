using AutoFixture;
using Bogus;
using Bogus.Extensions.Brazil;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Web.Controllers;
using PI.Web.Util;
using PI.Web.ViewModel.Machine;
using Visudata.Web.Test.Util;
using Xunit;

namespace Visudata.Web.Test
{
    public class MachineControllerTest
    {
        private readonly IMachineAppService _machineAppService;
        private readonly IMachineCategoryAppService _machineCategoryAppService;
        private MachineController _controller;
        private readonly Faker _faker;
        private string EnterpriseCnpj;
        private readonly IFixture _fixture;



        public MachineControllerTest()
        {
            _faker = new("pt_BR");
            _machineAppService = A.Fake<IMachineAppService>();
            _machineCategoryAppService = A.Fake<IMachineCategoryAppService>();
            this.EnterpriseCnpj = _faker.Company.Cnpj();
            _fixture = new Fixture().Customize(new IgnoreVirtualMembersCustomisation());
            _controller.Response.Cookies.Append("enterpriseCnpj", EnterpriseCnpj);
            _controller = new MachineController(_machineAppService, _machineCategoryAppService);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public async void Machines_will_returned()
        {

            List<Machine> machinesAsList = A.Fake<List<Machine>>();
            A.CallTo(() => _machineAppService.GetAllByCnpj(EnterpriseCnpj)).WithAnyArguments().Returns(machinesAsList);
            var result = await _controller.List();

            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public async void GetMachinesByCategory()
        {
            string categoryName = _faker.Random.AlphaNumeric(11);
            List<Machine> machineEnitty = new();
            A.CallTo(() => _machineAppService.GetAllByCategory(this.EnterpriseCnpj, categoryName)).WithAnyArguments().Returns(machineEnitty);

            var result = await _controller.GetMachinesForSpecificCategory(categoryName);

            //result.Should().BeOfType<ViewResult>();
            var viewResultObject = result as ViewResult;
            Assert.Equal(viewResultObject.Model, machineEnitty);
        }
        [Fact]
        public async void ListWillBeReturnedEntitiesAndViewModels()
        {
            List<Machine> models = new();
            models = _fixture.CreateMany<Machine>().ToList();
            models.ForEach(machineEntity =>
            {
                machineEntity.Logs = _fixture.CreateMany<Log>().ToList();
                machineEntity.Status = _fixture.Create<MachineStatus>();
                machineEntity.Category = _fixture.Create<MachineCategory>();
            });

            List<MachineForListViewModel> viewModels = new();

            A.CallTo(() => _machineAppService.GetAllByCnpj(EnterpriseCnpj)).WithAnyArguments().Returns(models);
            models.ForEach(machineEntity => viewModels.Add(HelperFunction.ConvertMachineToListViewModel(machineEntity)));

            var result = await _controller.List();

            var viewResultObject = result as ViewResult;
            var response = viewResultObject.Model as List<MachineForListViewModel>;

            response.Should().BeEquivalentTo(viewModels);
        }
        [Fact]
        public async void ThrowsExceptionOnConvertToviewModelForLogs()
        {
            List<Machine> models = new();

            models = _fixture.CreateMany<Machine>().ToList();
            A.CallTo(() => _machineAppService.GetAllByCnpj(EnterpriseCnpj)).WithAnyArguments().Returns(models);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.List());
        }
    }
}
