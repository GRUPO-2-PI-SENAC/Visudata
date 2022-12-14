using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PI.Application.Intefaces;
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
    public async Task<IActionResult> GetMachinesForSpecificCategory(string nameOfCategory)
    {
        string enterpriseOfCurrentSessionCnpj = Request.Cookies["enterpriseCnpj"];
        List<MachineForListViewModel> model = await _machineService.GetMachineOfSpecificCategory(enterpriseOfCurrentSessionCnpj, nameOfCategory);

        return View("List", model);

    }
    [HttpGet]
    public async Task<IActionResult> GetMachineForStatus(string status)
    {
        string enterpriseOfCurrentSessionCnpj = Request.Cookies["enterpriseCnpj"];
        List<MachineForListViewModel> machinesForList = await _machineService.GetMachineForStatus(enterpriseOfCurrentSessionCnpj, status);

        return View("List", machinesForList);
    }
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        List<string> nameOfCategoriesAsStringList = await _machineCategoryService.GetNameOfCategoriesAsString();

        List<SelectListItem> item = new List<SelectListItem>();

        foreach (string variable in nameOfCategoriesAsStringList)
        {
            item.Add(new SelectListItem(variable, variable));
        }

        ViewBag.MachineCategoriesAsStringList = item;
        return View(new AddMachineViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddMachineViewModel machineForAddInDb)
    {
        if (ModelState.IsValid)
        {
            string? enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
            bool isAdded = await _machineService.Add(machineForAddInDb, enterpriseCnpj);

            TempData["message"] = isAdded ? "Máquian adicionada com sucesso!!"
                : "Occorreu um erro tente novamente mais tarde ou entre em contato com o administrador";
            TempData["typeMessage"] = isAdded ? "success" : "error";

            return RedirectToAction("Home", "Enterprise");
        }
        return RedirectToAction("Add", machineForAddInDb);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        EditMachineViewModel modelForEditDataOfMachine = await _machineService.GetMachineDataForEdit(id);


        List<string> nameOfCategoriesAsStringList = await _machineCategoryService.GetNameOfCategoriesAsString();

        List<SelectListItem> item = new List<SelectListItem>();

        foreach (string variable in nameOfCategoriesAsStringList)
        {
            item.Add(new SelectListItem(variable, variable));
        }

        ViewBag.MachineCategoriesAsStringList = item;

        return View(modelForEditDataOfMachine);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditMachineViewModel editMachine)
    {
        if (ModelState.IsValid)
        {
            bool isUpdated = await _machineService.UpdateMachine(editMachine);

            TempData["message"] = isUpdated ? "Máquina atualizada com sucesso!" :
                "Occoreu um erro tente novamente mais tarde ou envie uma mensagem para a nossa equipe";
            TempData["typeMessage"] = isUpdated ? "success" : "error";
            return RedirectToAction("List");
        }
        return RedirectToAction("Edit");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        MachineDetailsViewModel model = await _machineService.GetMachineForDetails(id);
        return View(model);
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

        return isRegisted ? Ok("Added with success!") : BadRequest("Register doesn't added with expected, contact the administrator for more details");
    }

    public async Task<IActionResult> DownloadLogDataOfMachine(int id)
    {
        string dataAsCsv = await _machineService.GetHistoryDataByCsvByMachineId(id);

        if (dataAsCsv == "")
        {
            TempData["message"] = "A máquina não possui nenhum dado de histórico!";
            return RedirectToAction("List");
        }

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
    public async Task<IActionResult> DetailsAboutMachineAjaxHandler(int id, string status)
    {
        //string result = await _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, status);

        return Json("");
    }

    [HttpGet]
    [Route("[controller]/DetailsAboutTempAjaxHandler/{id}")]
    public async Task<JsonResult> DetailsAboutTempAjaxHandler(int id)
    {
        GraphicModel result = await _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, "temperatura");

        return Json(result);
    }
    [HttpGet]
    public async Task<JsonResult> DetailsAboutVibrationAjaxHandler(int id)
    {
        GraphicModel result = await _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, "vibracao");

        return Json(result);
    }
    [HttpGet]
    public async Task<JsonResult> DetailsAboutNoiseAjaxHandler(int id)
    {
        GraphicModel result = await _machineService.GetJsonForDetailsAboutMachineAjaxHandler(id, "ruido");

        return Json(result);
    }
    [HttpGet]
    public async Task<IActionResult> Register(int id)
    {
        List<RegisterMachineLogsViewModel> registers = await _machineService.GetRegisterAboutMachine(id);

        return View(registers);
    }

    #endregion
}