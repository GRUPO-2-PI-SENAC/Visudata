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


        //[HttpGet]
        //[Route("[controller]/getall")]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Json((await _machineAppService.GetMachinesForApiList("76535764000143")).ToList());
        //}

        //[HttpPost]
        //[Route("[controller]/add")]
        //public async Task<IActionResult> Add([FromBody] AddMachineViewModel model)
        //{
        //    bool isAdded = await _machineAppService.Add(model, "76535764000143");

        //    return isAdded ? Ok() : BadRequest("Error in create machine !");
        //}

        //[HttpGet]
        //[Route("[controller]/{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var machineForDetails = await _machineAppService.GetEditMachineModel(id);

        //    return Json(machineForDetails);
        //}

        //[HttpPut]
        //[Route("[controller]/update")]
        //public async Task<IActionResult> Update(EditMachineViewModel model)
        //{
        //    bool isUpdated = await _machineAppService.UpdateMachine(model);

        //    return isUpdated ? Ok("The machine are update with success!") : BadRequest("Some kind error occurred in update machine, conteact the adminsitrador");
        //}

        //[HttpDelete]
        //[Route("[controller]/delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    bool isRemoved = await _machineAppService.RemoveMachine(id);

        //    return isRemoved ? Ok() : BadRequest("Some kind of error occurred");
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
