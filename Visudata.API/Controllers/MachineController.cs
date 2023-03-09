using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.Entities;

namespace Visudata.API.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineAppService _machineAppService;

        public MachineController(IMachineAppService machineAppService)
        {
            _machineAppService = machineAppService;
        }

        [HttpGet("[controller]/getall")]
        public async Task<IActionResult> GetAll()
        {
            List<Machine> machines = await Task.FromResult(await _machineAppService.GetAll());
            return Json(machines);
        }

        [HttpGet("[controller]/getBycnpj/{cnpj}")]
        public async Task<IActionResult> GetByCnpj(string cnpj)
        {
            List<Machine> machinesByCnpj = await Task.FromResult(await _machineAppService.GetAllByCnpj(cnpj));
            return Json(machinesByCnpj);
        }

        [HttpGet("[controller]/getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Machine machineFromId = await Task.FromResult(await _machineAppService.GetById(id));
            return Json(machineFromId);
        }

        [HttpGet("[controller]/getBycnpjandcategory/{cnpj}/{category}")]
        public async Task<IActionResult> GetByCnpjAndCategory(string cnpj, string category)
        {
            List<Machine> machinesFromCategoryAndCnpj = await Task.FromResult(await _machineAppService.GetAllByCategory(cnpj, category));
            return Json(machinesFromCategoryAndCnpj);
        }


    }
}
