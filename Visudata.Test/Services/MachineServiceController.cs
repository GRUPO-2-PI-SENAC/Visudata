using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using PI.Application.Services;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;

namespace Visudata.Test.Services
{
    public class MachineServiceController
    {
        private readonly Mock<IMachineRepository> _machineRepository;
        private readonly Mock<IEnterpriseRepository> _enterpriseRepository;
        private readonly Mock<ILogsRepository> _logRepository;
        private readonly Mock<IMachineCategoryRepository> _machineCategoryRepository;
        private readonly Faker _faker;
        private readonly MachineServices _machineService;

        public MachineServiceController()
        {
            _machineRepository = new Mock<IMachineRepository>();
            _enterpriseRepository = new Mock<IEnterpriseRepository>();
            _logRepository = new Mock<ILogsRepository>();
            _machineCategoryRepository = new Mock<IMachineCategoryRepository>();
            _faker = new Faker();
            _machineService = new MachineServices(_machineRepository.Object, _enterpriseRepository.Object,
            _logRepository.Object, _machineCategoryRepository.Object);
        }

        [Fact]
        public async void MustBeReturnNotEmptyListInGetAll()
        {
            //Assert
            int enterpriseId = _faker.Random.Number(0, 9999);
            IEnumerable<Log> logs = Mock.Of<IEnumerable<Log>>();
            List<Machine> machines = Mock.Of<List<Machine>>();
            List<Log> logsAsList = Mock.Of<List<Log>>();


            _logRepository.Setup(service => service.GetAll()).ReturnsAsync(logs);
            _enterpriseRepository.Setup(enterprise => enterprise.GetAll()
            .Result.Any(enterprise => enterprise.Id == enterpriseId)).Returns(true);


            //act 

            var result = await _machineService.GetAll(enterpriseId);
            //Assert 

            result.Should().NotBeEmpty();
        }
    }
}