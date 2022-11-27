using Microsoft.AspNetCore.Mvc;
using PI.Application.Intefaces;
using PI.Domain.ViewModel.Enterpriqse;
using PI.Domain.ViewModel.Machine;

namespace PI.Web.Controllers;

public class MachineController : Controller
{
    #region Properties

    private readonly IMachineAppService _machineService;
    private readonly IMachineCategoryAppService _machineCategoryService;

    #endregion

    #region DIP 

    public MachineController(IMachineAppService machineService, IMachineCategoryAppService machineCategoryService)
    {
        _machineService = machineService;
        _machineCategoryService = machineCategoryService;
    }

    #endregion

    #region Actions

    [HttpGet]
    public async Task<IActionResult> List()
    {
        string enterpriseOfCurrentSessionCnpj = Request.Cookies["enterpriseCnpj"];

        List<MachineForListViewModel> model = await _machineService.GetMachinesByEnterpriseCnpj(enterpriseOfCurrentSessionCnpj);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        ViewBag.MachineCategoriesAsStringList = await _machineCategoryService.GetNameOfCategoriesAsString();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddMachineViewModel model)
    {
        string? enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
        bool isAdded = await _machineService.Add(model, enterpriseCnpj);

        TempData["message"] = isAdded ? "Máquian adicionada com sucesso!!"
            : "Occorreu um erro tente novamente mais tarde ou entre em contato com o administrador";

        return View("Enterprise", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        EditMachineViewModel modelForEditDataOfMachine = await _machineService.GetMachineDataForEdit(id);

        TempData["categories"] = await _machineCategoryService.GetNameOfCategoriesAsString();

        return View(modelForEditDataOfMachine);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditMachineViewModel model)
    {
        bool isUpdated = await _machineService.UpdateMachine(model);

        TempData["message"] = isUpdated ? "Máquina atualizada com sucesso!" :
            "Occoreu um erro tente novamente mais tarde ou envie uma mensagem para a nossa equipe";

        return RedirectToAction("List");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        bool isDeleted = await _machineService.RemoveMachine(id);

        TempData["message"] = isDeleted ? "Máquina apagada com sucesso!" :
            "Não foi possível deletar a máquina ,tente novamente mais tarde ou nos envie uma mensagem na tela de suporte";

        return RedirectToAction("List");
    }
    public async Task<IActionResult> SendDataFromSensors([FromBody] MachineDataRecieveFromSensorsJsonModel model)
    {
        bool isRegisted = await _machineService.AddRegisterOfMachineFromJson(model);

        return isRegisted ? Ok("Added with success!") : BadRequest("Register dosen't added with expected , contact the administrator for more details");
    }

    public async Task<IActionResult> DownloadLogDataOfMachine(int id)
    {
        string dataAsCsv = await _machineService.GetHistoryDataByCsvByMachineId(id);
        EditMachineViewModel machineForEdit = await _machineService.GetMachineDataForEdit(id);

        return File(System.Text.Encoding.UTF8.GetBytes(dataAsCsv), "text/csv", "data_da_maquina_" + machineForEdit.Model + ".csv");
    }
    [HttpGet]
    public async Task<IActionResult> DetailsAboutMachine(int id)
    {
        MachineDetailsViewModel model = await _machineService.GetMachineForDetails(id);

        return View(model);
    }

    [HttpGet]
    public async Task<JsonResult> DetailsAboutMachineAjaxHandler(int id, string status)
    {
        string result = await _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, status);

        return Json(result);
    }

    #endregion
}