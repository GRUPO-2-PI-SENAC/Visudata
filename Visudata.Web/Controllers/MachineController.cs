using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Machine;
using System.Net;

namespace PI.Web.Controllers;

public class MachineController : Controller
{
    #region Properties

    private readonly IMachineService _machineService;
    private readonly IMachineCategoryService _machineCategoryService;

    #endregion

    #region DIP 

    public MachineController(IMachineService machineService, IMachineCategoryService machineCategoryService)
    {
        _machineService = machineService;
        _machineCategoryService = machineCategoryService;
    }

    #endregion

    #region Actions

    [HttpGet]
    public async Task<IActionResult> List(int enterpriseId)
    {
        List<MachinesForListViewModel> model = await _machineService.GetMachinesByEnterpriseId(enterpriseId);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.MachineCategoriesAsStringList = await _machineCategoryService.GetNameOfCategoriesAsString();
        return View();
    }

    public async Task<IActionResult> Add(AddMachineViewModel model)
    {
        string? enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
        bool isAdded = await _machineService.Add(model, enterpriseCnpj);

        TempData["message"] = isAdded ? "Máquian adicionada com sucesso!!" 
            : "Occorreu um erro tente novamente mais tarde ou entre em contato com o administrador";

        return View("Enterprise", "Home");
    }

    public async Task<IActionResult> SendDataFromSensors([FromBody] MachineDataRecieveFromSensorsJsonModel model)
    {
        bool isRegisted = await _machineService.AddRegisterOfMachineFromJson(model);

        return isRegisted ? Ok("Added with success!") : BadRequest("Register dosen't added with expected , contact the administrator for more details");
    }
    #endregion

}