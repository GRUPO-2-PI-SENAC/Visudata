using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Web.Util;
using PI.Web.ViewModel.Machine;
using System.Reflection.PortableExecutable;
using Machine = PI.Domain.Entities.Machine;

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

        List<Machine> machinesEntity = await _machineService.GetAllByCnpj(enterpriseOfCurrentSessionCnpj);

        List<MachineForListViewModel> model = new();

        machinesEntity.ForEach(machine =>
        {
            model.Add(HelperFunction.ConvertMachineToListViewModel(machine));
        });

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetMachinesForSpecificCategory(string nameOfCategory)
    {
        string enterpriseOfCurrentSessionCnpj = Request.Cookies["enterpriseCnpj"];
        List<Machine> model = await _machineService.GetAllByCategory(enterpriseOfCurrentSessionCnpj, nameOfCategory);



        return View("List", model);

    }
    [HttpGet]
    public async Task<IActionResult> GetMachineForStatus(string status)
    {
        string enterpriseOfCurrentSessionCnpj = Request.Cookies["enterpriseCnpj"];
        List<Machine> machinesForList = await _machineService.GetByStatus(enterpriseOfCurrentSessionCnpj, status);


        List<MachineForListViewModel> model = new();

        machinesForList.ForEach(machine =>
        {
            model.Add(HelperFunction.ConvertMachineToListViewModel(machine));
        });

        return View("List", model);
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
            Machine machineEntity = new();
            machineForAddInDb.ConvertToEntity(machineEntity);
            bool isAdded = await _machineService.Add(machineEntity, enterpriseCnpj);

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
        Machine modelForEditDataOfMachine = await _machineService.GetById(id);


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
            Machine machine = new();
            editMachine.ConvertToEntity(machine);
            bool isUpdated = await _machineService.Update(machine);

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
        Machine model = await _machineService.GetById(id);
        return View(model);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        bool isDeleted = await _machineService.Delete(id);

        TempData["message"] = isDeleted ? "Máquina apagada com sucesso!" :
            "Não foi possível deletar a máquina ,tente novamente mais tarde ou nos envie uma mensagem na tela de suporte";

        return RedirectToAction("List");
    }
    public async Task<IActionResult> SendDataFromSensors([FromBody] MachineDataRecieveFromSensorsJsonModel model)
    {
        bool isRegisted = await _machineService.AddRegister(model.MachineId, model.Temp, model.Noise, model.Vibration);

        return isRegisted ? Ok("Added with success!") : BadRequest("Register doesn't added with expected, contact the administrator for more details");
    }
    public async Task<IActionResult> DownloadLogDataOfMachine(int id)
    {
        Domain.Entities.Machine machineEntity = await _machineService.GetById(id);

        string machineDataAsCsv = GetMachineLogDataAsCsvString(machineEntity);

        if (machineDataAsCsv == "")
        {
            TempData["message"] = "A máquina não possui nenhum dado de histórico!";
            return RedirectToAction("List");
        }

        return File(System.Text.Encoding.UTF8.GetBytes(machineDataAsCsv), "text/csv", "data_da_maquina_" + machineEntity.Model + ".csv");
    }
    [HttpGet]
    public async Task<IActionResult> DetailsAboutMachine(int id)
    {
        Domain.Entities.Machine model = await _machineService.GetById(id);

        MachineDetailsViewModel viewModel = new();

        viewModel.ReceiveDataFromEntity(model);

        return View(viewModel);
    }
    [HttpGet]
    [Route("[controller]/DetailsAboutTempAjaxHandler/{id}")]
    public async Task<JsonResult> DetailsAboutTempAjaxHandler(int id)
    {
        Machine machineEntity = await _machineService.GetById(id);
        GraphicModel result = new();
        result.GetDataFromMachine("temp", machineEntity);
        return Json(result);
    }
    [HttpGet]
    public async Task<JsonResult> DetailsAboutVibrationAjaxHandler(int id)
    {
        Machine machineEntity = await _machineService.GetById(id);
        GraphicModel result = new();
        result.GetDataFromMachine("vibration", machineEntity);
        return Json(result);
    }
    [HttpGet]
    public async Task<JsonResult> DetailsAboutNoiseAjaxHandler(int id)
    {
        Machine machineEntity = await _machineService.GetById(id);
        GraphicModel result = new();
        result.GetDataFromMachine("vibration", machineEntity);

        return Json(result);
    }
    [HttpGet]
    public async Task<IActionResult> Register(int id)
    {
        Machine machineForRegister = await _machineService.GetById(id);

        List<RegisterMachineLogsViewModel> viewModel = ConvertToLogs(machineForRegister);

        return View(viewModel);
    }
    private List<RegisterMachineLogsViewModel> ConvertToLogs(Machine machineEntity)
    {
        List<RegisterMachineLogsViewModel> machineLogs = new();

        machineEntity.Logs.ForEach(log =>
        {
            machineLogs.Add(new RegisterMachineLogsViewModel
            {
                Vibration = log.Vibration,
                Noise = log.Noise,
                Temp = log.Temp,
                DateAsString = log.Created_at.ToString("dd/MM/yyyy"),
                HourAsString = log.Created_at.ToString("HH/mm")
            });
        });

        return machineLogs;
    }
    private string GetMachineLogDataAsCsvString(Domain.Entities.Machine machineEntity)
    {

        List<Log> logsOfMachine = machineEntity.Logs;

        if (!logsOfMachine.Any() || logsOfMachine.Count() == 0)
        {
            return "";
        }

        string csv = "";

        csv += "HORA;VIBRACAO;RUIDO;TEMPERATURA\n";

        foreach (Log log in logsOfMachine)
        {
            csv += log.Created_at.Hour + ";" + log.Vibration.ToString() + ";" + log.Noise.ToString() + ";" + log.Temp.ToString() + "\n";
        }

        return csv;
    }

    #endregion
}