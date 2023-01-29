using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.Entities;
using PI.Web.ViewModel.Enterprise;
using PI.Web.ViewModel.UserSupport;

namespace PI.Web.Controllers
{
    public class EnterpriseController : Controller
    {
        #region Properties
        private IEnterpriseAppService _enterpriseService;
        private readonly IUserSupportAppService _userSupportService;
        private readonly IUserProblemsCategoryAppService _userProblemsCategoryService;
        private readonly IMachineAppService _machineAppService;
        #endregion

        #region DIP
        public EnterpriseController(IEnterpriseAppService enterpriseService, IUserSupportAppService userSupportService, IUserProblemsCategoryAppService userProblemsCategoryService, IMachineAppService machineAppService)
        {
            _enterpriseService = enterpriseService;
            _userSupportService = userSupportService;
            _userProblemsCategoryService = userProblemsCategoryService;
            _machineAppService = machineAppService;
        }
        #endregion

        #region Actions 

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            string enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
            List<Machine> machines = await _machineAppService.GetAllByCnpj(enterpriseCnpj);
            AmountOfMachinesStatusByEnterpriseViewModel model = new();
            model.ExtractDataFromMachines(machines);

            return View(model);
        }

        public async Task<IActionResult> Settings()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string enterpriseForRegisterAsString)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateEnterpriseViewModel? create = JsonConvert.DeserializeObject<CreateEnterpriseViewModel>(enterpriseForRegisterAsString);

                    if (create == null)
                    {
                        TempData["message"] = "Ocorreu um erro para criar um cadastro tente novamente mais tarde , ou entre em contato !";

                        return View();
                    }
                    Domain.Entities.Enterprise entity = new();
                    create.ConvertToEntity(entity);
                    bool isValid = await _enterpriseService.SignUp(entity);

                    if (isValid)
                    {
                        return View("Redirect");
                    }

                    return View();
                }
                catch
                {
                    TempData["message"] = "Não foi possível criar entre em contato com o administrador";
                    return View();
                }
            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> ViewProfie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEnterprise(UpdateEnterpriseViewModel model)
        {
            Enterprise entity = new();
            model.ConvertToEntity(entity);
            bool isUpdated = await _enterpriseService.Update(entity);

            TempData["message"] = isUpdated ? "Seus dados foram atualizados com sucesso !" : "Não foi possível fazer a alteração de seus dados tente novamente mais tarde ou mande um recado na tela de suporte";

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(EnterpriseLoginViewModel enterpriseForLogin)
        {
            if (ModelState.IsValid)
            {
                bool isLogin = await _enterpriseService.Login(enterpriseForLogin.Login, enterpriseForLogin.Password);
                if (isLogin)
                {
                    TempData["message"] = "Usuario adicionado com sucesso!";
                    Response.Cookies.Append("enterpriseCnpj", enterpriseForLogin.Login);
                    return RedirectToAction("Home");
                }

                ModelState.AddModelError(nameof(enterpriseForLogin.Password), "Senha ou usuário incorretos");

                return View();
            }
            ModelState.AddModelError(nameof(enterpriseForLogin.Login), "Usuário ou senha inválidos");
            return View(enterpriseForLogin);
        }

        [HttpGet]
        public async Task<IActionResult> Support()
        {
            ViewBag.userProblemsCategoriesAsString = await _userProblemsCategoryService.GetNameOfAllAsString();
            string enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
            Enterprise currentEnterprise = await _enterpriseService.GetByCnpj(enterpriseCnpj);
            AddUserSupportViewModel modelForView = new AddUserSupportViewModel() { EnterpriseId = currentEnterprise.Id, NameOfEnterprise = currentEnterprise.FantasyName };
            TempData["enterpriseId"] = currentEnterprise.Id.ToString();
            return View(modelForView);

        }

        [HttpPost]
        public async Task<IActionResult> Support(AddUserSupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                string enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
                Enterprise currentEnterprise = await _enterpriseService.GetByCnpj(enterpriseCnpj);
                model.EnterpriseId = currentEnterprise.Id;
                UserSupport entity = new();
                model.ConvertToEntity(entity);
                bool isCreated = await _userSupportService.CreateUserReport(entity);

                if (isCreated)
                {
                    TempData["message"] = "Relato enviado com sucesso!";
                    return RedirectToAction("Home");
                }
                TempData["message"] = "Näo foi possível adicionar esse relato , tente novamente mais tarde ou entre em contato aocm o administrador";
                ViewBag.userProblemsCategoriesAsString = await _userProblemsCategoryService.GetNameOfAllAsString();
                return RedirectToAction("Support");
            }
            ModelState.AddModelError(nameof(model.ProblemDescription), "Por favor insira valores válidos!");
            ViewBag.userProblemsCategoriesAsString = await _userProblemsCategoryService.GetNameOfAllAsString();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
            Enterprise entity = await _enterpriseService.GetByCnpj(enterpriseCnpj);

            //EnterpriseProfileViewModel currentEnterprise = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj);
            EnterpriseProfileViewModel viewModel = new();
            viewModel.GetDataFromEntity(entity);
            return View(viewModel);
        }

        public async Task<IActionResult> Menu()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("enterpriseCnpj");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Redirect()
        {
            return View();
        }
        #endregion
    }
}
