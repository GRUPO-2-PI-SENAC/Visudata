using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PI.Application.Intefaces;
using PI.Domain.ViewModel.Machine;
using PI.Web.Controllers;

namespace Visudata.Test.WebControllers
{
    public class MachineControllerTest
    {
        private readonly Mock<IMachineAppService> _machineAppService;
        private readonly Mock<IMachineCategoryAppService> _machineCategoryAppService;

        private readonly Faker _fake;

        public MachineControllerTest()
        {
            _machineAppService = new Mock<IMachineAppService>();
            _machineCategoryAppService = new Mock<IMachineCategoryAppService>();
            _fake = new Faker();
        }

        [Fact]
        public async void MustBeReturnedListOnGet()
        {
            //Arrange 
            string enterpriseCnpj = _fake.Company.Cnpj();
            Mock<List<MachineForListViewModel>> machinesReturned = new Mock<List<MachineForListViewModel>>();
            _machineAppService.Setup(service => service.GetMachinesByEnterpriseCnpj(enterpriseCnpj)).ReturnsAsync(machinesReturned.Object);

            var httpContext = new DefaultHttpContext(); // or mock a `HttpContext`
            httpContext.Request.Headers["token"] = "fake_token_here"; //Set header
            httpContext.Response.Cookies.Append("enterpriseCnpj", enterpriseCnpj);
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            //assign context to controller
            var controller = new MachineController(_machineAppService.Object, _machineCategoryAppService.Object)
            {
                ControllerContext = controllerContext,
            };

            //Act 
            var result = await controller.List();

            //Assert 

            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void EnterpriseDontHaveAnyMachine()
        {
            //Assert
            string enterpriseCnpj = "";
            List<MachineForListViewModel> machines = new List<MachineForListViewModel>();
            _machineAppService.Setup(service => service.GetMachinesByEnterpriseCnpj(enterpriseCnpj)).ReturnsAsync(machines);
            var httpContext = new DefaultHttpContext(); // or mock a `HttpContext`
            httpContext.Request.Headers["token"] = "fake_token_here"; //Set header
            httpContext.Response.Cookies.Append("enterpriseCnpj", enterpriseCnpj);
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            //assign context to controller
            var controller = new MachineController(_machineAppService.Object, _machineCategoryAppService.Object)
            {
                ControllerContext = controllerContext,
            };

            //Act

            var result = await controller.List();
            //Assert
            result.Should().BeOfType<ViewResult>();
        }


    }
}