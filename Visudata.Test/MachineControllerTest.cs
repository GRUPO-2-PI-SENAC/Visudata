using AutoFixture;
using Bogus;
using Bogus.Extensions.Brazil;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Web.Controllers;
using PI.Web.Util;
using PI.Web.ViewModel.Enterprise;
using PI.Web.ViewModel.Machine;
using System.Diagnostics.CodeAnalysis;
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
        private readonly Enterprise _currentEnterprise;
        private string _enterpriseCnpj;
        private readonly IFixture _fixture;



        public MachineControllerTest()
        {
            _faker = new("pt_BR");
            _machineAppService = A.Fake<IMachineAppService>();
            _machineCategoryAppService = A.Fake<IMachineCategoryAppService>();
            _fixture = new Fixture().Customize(new IgnoreVirtualMembersCustomisation());
            _currentEnterprise = _fixture.Create<Enterprise>();
            _enterpriseCnpj = _currentEnterprise.Cnpj;

            _controller = new MachineController(_machineAppService, _machineCategoryAppService);
            Mock<ITempDataDictionary> tempDataAsMock = new Mock<ITempDataDictionary>();

            _controller.TempData = tempDataAsMock.Object;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Session = MockSession();
            _controller.Response.Cookies.Append("enterpriseCnpj", _enterpriseCnpj);


        }

        [Fact]
        public async Task Machines_will_returned()
        {

            List<Machine> machinesAsList = A.Fake<List<Machine>>();
            A.CallTo(() => _machineAppService.GetAllByCnpj(_enterpriseCnpj)).WithAnyArguments().Returns(machinesAsList);
            var result = await _controller.List();

            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public async Task GetMachinesByCategory()
        {
            string categoryName = _faker.Random.AlphaNumeric(11);
            List<Machine> machineEnitty = new();
            A.CallTo(() => _machineAppService.GetAllByCategory(this._enterpriseCnpj, categoryName)).WithAnyArguments().Returns(machineEnitty);

            var result = await _controller.GetMachinesForSpecificCategory(categoryName);

            //result.Should().BeOfType<ViewResult>();
            var viewResultObject = result as ViewResult;
            Assert.Equal(viewResultObject.Model, machineEnitty);
        }
        [Fact]
        public async Task ListWillBeReturnedEntitiesAndViewModels()
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

            A.CallTo(() => _machineAppService.GetAllByCnpj(_enterpriseCnpj)).WithAnyArguments().Returns(models);
            models.ForEach(machineEntity => viewModels.Add(HelperFunction.ConvertMachineToListViewModel(machineEntity)));

            var result = await _controller.List();

            var viewResultObject = result as ViewResult;
            var response = viewResultObject.Model as List<MachineForListViewModel>;

            response.Should().BeEquivalentTo(viewModels);
        }

        [Fact]
        public async Task ThrowsExceptionOnConvertToviewModelForLogs()
        {
            List<Machine> models = new();

            models = _fixture.CreateMany<Machine>().ToList();
            A.CallTo(() => _machineAppService.GetAllByCnpj(_enterpriseCnpj)).WithAnyArguments().Returns(models);
            await Assert.ThrowsAsync<ArgumentNullException>(() => _controller.List());
        }

        [Fact]
        public async Task ShouldReturnMachinesByStatus()
        {
            string status = _fixture.Create<string>();
            List<Machine> machinesAsList = _fixture.Create<List<Machine>>();
            machinesAsList.ForEach(machine => { machine.Status = _fixture.Create<MachineStatus>(); machine.Logs = _fixture.Create<List<Log>>(); });
            A.CallTo(() => _machineAppService.GetByStatus(_currentEnterprise.Cnpj, status)).Returns(machinesAsList);

            var result = await _controller.GetMachineForStatus(status);

            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task ShouldReturnedSameObjectExpectedOfViewModelFromMachinesByStatus()
        {
            List<Machine> machines = _fixture.Create<List<Machine>>();
            string machineStatusForSeach = _fixture.Create<string>();
            machines.ForEach(machine => { machine.Logs = _fixture.Create<List<Log>>(); machine.Status = _fixture.Create<MachineStatus>(); machine.Category = _fixture.Create<MachineCategory>(); });
            List<MachineForListViewModel> machinesForListViewModel = new();
            machines.ForEach(machine => machinesForListViewModel.Add(HelperFunction.ConvertMachineToListViewModel(machine)));

            A.CallTo(() => _machineAppService.GetByStatus(_currentEnterprise.Cnpj, machineStatusForSeach)).WithAnyArguments().Returns(machines);

            var result = await _controller.GetMachineForStatus(machineStatusForSeach);

            ViewResult convertResultTo = result as ViewResult;
            List<MachineForListViewModel> modelFromViewReuslt = convertResultTo.Model as List<MachineForListViewModel>;
            string serializeResultViewModle = JsonConvert.SerializeObject(modelFromViewReuslt);
            string serializeExpectedResult = JsonConvert.SerializeObject(machinesForListViewModel);
            Assert.Equal(serializeExpectedResult, serializeResultViewModle);
        }

        [Fact]
        public async Task ShouldReturnedTheSameValuesOfVibrationNoiseAndTemp()
        {
            List<Machine> machines = _fixture.Create<List<Machine>>();
            string machineStatusForSeach = _fixture.Create<string>();
            machines.ForEach(machine => { machine.Logs = _fixture.Create<List<Log>>(); machine.Status = _fixture.Create<MachineStatus>(); machine.Category = _fixture.Create<MachineCategory>(); });
            List<MachineForListViewModel> machinesForListViewModel = new();
            machines.ForEach(machine => machinesForListViewModel.Add(HelperFunction.ConvertMachineToListViewModel(machine)));

            A.CallTo(() => _machineAppService.GetByStatus(_currentEnterprise.Cnpj, machineStatusForSeach)).WithAnyArguments().Returns(machines);

            var result = await _controller.GetMachineForStatus(machineStatusForSeach);

            ViewResult convertResultTo = result as ViewResult;
            List<MachineForListViewModel> modelFromViewReuslt = convertResultTo.Model as List<MachineForListViewModel>;

            bool hasTheSameTrheeMainPropertiesValue = machinesForListViewModel.All(machine =>
            {
                MachineForListViewModel machineForCompare = modelFromViewReuslt.First(mw => mw.Id == machine.Id);

                if (machineForCompare.Noise == machine.Noise && machineForCompare.Vibration == machine.Vibration && machineForCompare.Temp == machine.Temp)
                    return true;
                return false;
            }
            );

            hasTheSameTrheeMainPropertiesValue.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldThrowsExceptionIfMachineLogsAreNullOnGetByStatus()
        {
            List<Machine> machinesAsList = _fixture.Create<List<Machine>>();
            machinesAsList.ForEach(machine => machine.Status = _fixture.Create<MachineStatus>());
            string statusForSearch = _fixture.Create<string>();

            A.CallTo(() => _machineAppService.GetByStatus(_currentEnterprise.Cnpj, statusForSearch)).WithAnyArguments().Returns(machinesAsList);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _controller.GetMachineForStatus(statusForSearch));
        }

        [Fact]
        public async Task MachineShouldBeAddAndReturnRedirectToActionResultOnAddAction()
        {
            AddMachineViewModel viewModel = _fixture.Create<AddMachineViewModel>();
            Machine entityFromViewModelConvert = new();
            viewModel.ConvertToEntity(entityFromViewModelConvert);
            A.CallTo(() => _machineAppService.Add(entityFromViewModelConvert, _currentEnterprise.Cnpj)).Returns(true);

            var result = await _controller.Add(viewModel);

            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task ShouldReturnViewResultWithViewModelIfModelStateAreInvalid()
        {
            AddMachineViewModel viewModel = _fixture.Create<AddMachineViewModel>();
            _controller.ModelState.AddModelError("Model", "Caracteres especiais nâo são permitidos");

            var result = await _controller.Add(viewModel);

            result.Should().BeOfType<ViewResult>();
            ViewResult resultFromController = result as ViewResult;

            resultFromController.Model.Should().BeSameAs(viewModel);
        }

        [Fact]
        public async Task ShouldMachineUpdated()
        {
            // Setup the brand variable manually, because libary don´t support mock data with regular expressions .therefore must be set attribute value
            _fixture.Customize<EditMachineViewModel>(c => c.With(vm => vm.Brand, "Anonymous Brand for tester"));
            EditMachineViewModel editMachine = _fixture.Create<EditMachineViewModel>();

            A.CallTo(() => _machineAppService.Update(new Machine())).WithAnyArguments().Returns(true);

            var result = await _controller.Edit(editMachine);

            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task ShouldReturnViewAndViewModelWhenModelStateAreInvalid()
        {
            _fixture.Customize<EditMachineViewModel>(c => c.With(vm => vm.Brand, "With any brand value"));
            EditMachineViewModel viewModel = _fixture.Create<EditMachineViewModel>();
            _controller.ModelState.AddModelError("model", "Invalid result");
            var result = await _controller.Edit(viewModel);
            result.Should().BeOfType<ViewResult>();
            ViewResult resultAsViewResult = result as ViewResult;
            resultAsViewResult.Model.Should().BeSameAs(viewModel);
        }

        [Fact]
        public async Task ShouldReturnRedirectToActionIfDontUpdateMachineWithSuccessfulOnEdit()
        {
            _fixture.Customize<EditMachineViewModel>(c => c.With(vm => vm.Brand, "With any brand value"));
            EditMachineViewModel viewModel = _fixture.Create<EditMachineViewModel>();
            A.CallTo(() => _machineAppService.Update(new Machine())).WithAnyArguments().Returns(false);

            var result = await _controller.Edit(viewModel);

            result.Should().BeOfType<RedirectToActionResult>();
        }


        [ExcludeFromCodeCoverage]
        private ISession MockSession()
        {
            Mock<ISession> mockOfCurrentSession = new();
            mockOfCurrentSession.Object.SetString("currentEnterprise", JsonConvert.SerializeObject(_currentEnterprise));
            return mockOfCurrentSession.Object;

        }
    }
}
