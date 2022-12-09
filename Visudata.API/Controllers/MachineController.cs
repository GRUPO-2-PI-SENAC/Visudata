using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.ViewModel.Machine;

namespace Visudata.API.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineAppService _machineAppService;

        public MachineController(IMachineAppService machineAppService)
        {
            _machineAppService = machineAppService;
        }

        [HttpGet]
        [Route("[controller]/getall")]
        public async Task<IActionResult> GetAll()
        {
            return Json((await _machineAppService.GetAll(6203)).ToList());
        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> Add([FromBody] AddMachineViewModel model)
        {
            bool isAdded = await _machineAppService.Add(model, "76535764000143");

            return isAdded ? Ok() : BadRequest("Error in create machine !");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
