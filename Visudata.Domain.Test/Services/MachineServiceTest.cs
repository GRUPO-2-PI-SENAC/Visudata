using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Bogus;
using FakeItEasy;
using FluentAssertions;
using PI.Application.Intefaces;
using PI.Application.Services;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;
using Visudata.Domain.Test.Utils;

namespace Visudata.Domain.Test.Services
{
    public class MachineServiceTest
    {
        private readonly IMachineService _machineService;
        private readonly ILogsRepository _logsRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IOutlierRegisterRepository _outlierRegisterRepository;
        private readonly IMachineCategoryRepository _machineCategoryRepository;
        private readonly IFixture _fixture;
        private Faker _faker;

        public MachineServiceTest()
        {
            _machineRepository = A.Fake<IMachineRepository>();
            _enterpriseRepository = A.Fake<IEnterpriseRepository>();
            _logsRepository = A.Fake<ILogsRepository>();
            _outlierRegisterRepository = A.Fake<IOutlierRegisterRepository>();
            _machineCategoryRepository = A.Fake<IMachineCategoryRepository>();
            _faker = new Faker();
            _fixture = new Fixture().Customize(new IgnoreVirtualMembersCustomisation());
            _machineService = new MachineServices(_machineRepository, _enterpriseRepository, _logsRepository, _machineCategoryRepository, _outlierRegisterRepository);
        }

        [Fact]
        public async void MachineShouldBeAdded()
        {
            Machine entity = _fixture.Create<Machine>();
            MachineCategory machineCt = _fixture.Create<MachineCategory>();
            Enterprise enterprise = _fixture.Create<Enterprise>();

            entity.Category = machineCt;

            A.CallTo(() => _machineCategoryRepository.GetByName(machineCt.Name)).Returns(machineCt);
            A.CallTo(() => _enterpriseRepository.GetEnterpriseByCnpj(enterprise.Cnpj)).Returns(enterprise);
            A.CallTo(() => _machineRepository.Add(entity));
            var result = await _machineService.Add(entity, enterprise.Cnpj);

            result.Should().BeTrue();
        }

        [Fact]
        public async void MachineShouldBeUpdated()
        {
            Machine entity = _fixture.Create<Machine>();
            entity.Category = _fixture.Create<MachineCategory>();
            Machine forUpdate = _fixture.Create<Machine>();
            MachineCategory category = _fixture.Create<MachineCategory>();

            A.CallTo(() => _machineRepository.GetById(entity.Id)).Returns(forUpdate);
            A.CallTo(() => _machineCategoryRepository.GetByName(entity.Category.Name)).Returns(category);
            A.CallTo(() => _machineRepository.Update(forUpdate));

            var result = await _machineService.Update(entity);

            result.Should().BeTrue();
        }

        [Fact]
        public async void OnUpdateMachineNewValuesPropertiesShouldBeSame()
        {
            Machine entity = _fixture.Create<Machine>();
            Machine forUpdate = _fixture.Create<Machine>();
            entity.Category = _fixture.Create<MachineCategory>();

            A.CallTo(() => _machineRepository.GetById(entity.Id)).Returns(forUpdate);
            A.CallTo(() => _machineCategoryRepository.GetByName(entity.Category.Name)).Returns(entity.Category);
            A.CallTo(() => _machineRepository.Update(forUpdate));

            var result = await _machineService.Update(entity);

            result.Should().BeTrue();
            entity.Category.Should().BeSameAs(forUpdate.Category);
            entity.NoiseMax.Should().Be(forUpdate.NoiseMax);
            entity.NoiseMin.Should().Be(forUpdate.NoiseMin);
            entity.VibrationMax.Should().Be(forUpdate.VibrationMax);
            entity.VibrationMin.Should().Be(forUpdate.VibrationMin);
            entity.TempMax.Should().Be(forUpdate.TempMax);
            entity.TempMin.Should().Be(forUpdate.TempMin);
            entity.Brand.Should().BeSameAs(forUpdate.Brand);
            entity.SerialNumber.Should().Be(forUpdate.SerialNumber);
            entity.Tag.Should().BeSameAs(forUpdate.Tag);

        }
    }
}
