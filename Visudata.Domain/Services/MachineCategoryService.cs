﻿using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Domain.Interfaces.Repositories;

namespace PI.Application.Services
{
    public class MachineCategoryService : IMachineCategoryService
    {
        private readonly IMachineCategoryRepository _machineCategoryRepository;

        public MachineCategoryService(IMachineCategoryRepository machineCategoryRepository)
        {
            _machineCategoryRepository = machineCategoryRepository;
        }

        public async Task<List<string>> GetNameOfCategoriesAsString()
        {
            try
            {
                List<MachineCategory> machinesCategoriesInDb = _machineCategoryRepository.GetAll().Result.ToList();

                List<string> namesOfMachinesCategories = new List<string>();

                foreach (MachineCategory machineCategory in machinesCategoriesInDb)
                {
                    namesOfMachinesCategories.Add(machineCategory.Name);
                }

                return namesOfMachinesCategories;
            }
            catch
            {
                return new List<string>();

            }
        }
    }
}
