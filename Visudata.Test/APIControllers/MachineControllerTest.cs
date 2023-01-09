using Bogus;
using Bogus.Extensions.Brazil;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.ViewModel.Machine;
using Visudata.API.Controllers;

namespace Visudata.Test.APIControllers
{
    public class MachineControllerTest
    {
        private IMachineAppService _machineAppService;
        private MachineController _machineController;
        private Faker _fake;

        public MachineControllerTest()
        {
            _machineAppService = A.Fake<IMachineAppService>();
            _machineController = new MachineController(_machineAppService);
            _fake = new Faker();
        }
        [Fact]
        public async void MustReturnedMachines()
        {
            //arrage
            var machines = FakeItEasy.A.Fake<IEnumerable<MachineForAPIListViewModel>>();
            //act 
            var result = await _machineController.GetAll();

            //Assert 
            Assert.NotNull(result);

        }

        [Fact]
        public async void GetAll()
        {
            var machines = A.Fake<List<MachineForAPIListViewModel>>();
            string enterpriseCnpj = _fake.Company.Cnpj();
            A.CallTo(() => _machineAppService.GetMachinesForApiList(enterpriseCnpj)).Returns(machines);
            var result = (JsonResult)await _machineController.GetAll();
            var values = result.Value as List<MachineForAPIListViewModel>;
            Assert.Equal(machines.Count(), values.Count());

        }

        [Fact]
        public async void GetAllCannotBeNull()
        {
            int enterpriseId = _fake.Random.Int(1);
            A.CallTo(() => _machineAppService.GetAll(enterpriseId)).Returns(new List<MachineForListViewModel>());
            JsonResult result = (JsonResult)await _machineController.GetAll();
            Assert.NotNull(result);
        }

        [Fact]
        public async void CannotAddMachine()
        {
            bool resultForAddMachine = _fake.Random.Bool();
            AddMachineViewModel model = A.Fake<AddMachineViewModel>();
            string enterpriseCnpj = _fake.Company.Cnpj();
            int enterpriseId = _fake.Random.Int(1);
            int machineId = _fake.Random.Int(1);
            EditMachineModel machineById = A.Fake<EditMachineModel>();
            model.Brand = machineById.Brand;
            model.SerialNumber = machineById.SerialNumber;
            model.Category = machineById.Category;

            A.CallTo(() => _machineAppService.GetEditMachineModel(machineId)).Returns(machineById);
            A.CallTo(() => _machineAppService.Add(model, enterpriseCnpj)).Returns(resultForAddMachine);
            var result = await _machineController.Add(model);

            result.Should().BeOfType<BadRequestObjectResult>();
            Assert.False(resultForAddMachine);

        }

        [Fact]
        public async void GetById()
        {
            int machineId = _fake.Random.Int(1);
            EditMachineModel model = A.Fake<EditMachineModel>();

            A.CallTo(() => _machineAppService.GetEditMachineModel(machineId)).Returns(model);
            JsonResult result = await _machineController.GetById(machineId) as JsonResult;
            EditMachineModel controllerResult = result.Value as EditMachineModel;

            Assert.Equal(controllerResult, model);
        }

        [Fact]
        public async void MustAddMachine()
        {
            var machine = A.Fake<AddMachineViewModel>();
            string cnpj = _fake.Company.Cnpj();
            A.CallTo(() => _machineAppService.Add(machine, cnpj)).Returns(true);
            var result = _machineController.Add(machine);
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public async void CannotUpdateMachine()
        {
            var machinesList = A.Fake<List<MachineForListViewModel>>();
            var machine = A.Fake<EditMachineViewModel>();
            string cnpj = _fake.Company.Cnpj();
            A.CallTo(() => _machineAppService.GetAll(2)).Returns(machinesList);

            machinesList.Should().NotContain(a => a.Id == machine.MachineId);
            A.CallTo(() => _machineAppService.UpdateMachine(machine)).Returns(false);

            var result = _machineController.Update(machine);

            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public async void MachineMustBeUpdated()
        {
            EditMachineViewModel model = A.Fake<EditMachineViewModel>();

            A.CallTo(() => _machineAppService.UpdateMachine(model)).Returns(true);

            var result = await _machineController.Update(model);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllMustHas10Machines()
        {
            List<MachineForListViewModel> model = A.Fake<List<MachineForListViewModel>>();
            int enterpriseId = _fake.Random.Int(1);

            A.CallTo(() => _machineAppService.GetAll(enterpriseId)).Returns(model);

            model.Should().HaveCount(10);

        }


    }
}
