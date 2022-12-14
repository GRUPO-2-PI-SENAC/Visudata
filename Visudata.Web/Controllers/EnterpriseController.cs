using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Domain.ViewModel.Enterpriqse;
using PI.Domain.ViewModel.Enterprise;
using PI.Domain.ViewModel.UserSupport;

namespace PI.Web.Controllers
{
    public class EnterpriseController : Controller
    {
        #region Properties
        private IEnterpriseAppService _enterpriseService;
        private readonly IUserSupportAppService _userSupportService;
        private readonly IUserProblemsCategoryAppService _userProblemsCategoryService;
        #endregion

        #region DIP
        public EnterpriseController(IEnterpriseAppService enterpriseService, IUserSupportAppService userSupportService, IUserProblemsCategoryAppService userProblemsCategoryService)
        {
            _enterpriseService = enterpriseService;
            _userSupportService = userSupportService;
            _userProblemsCategoryService = userProblemsCategoryService;
        }
        #endregion

        #region Actions 

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            string enterpriseCnpj = Request.Cookies["enterpriseCnpj"].ToString();
            AmountOfMachinesStatusByEnterpriseViewModel model = await _enterpriseService.GetMachinesStatusByEnterpriseCnpj(enterpriseCnpj);

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

                    bool isValid = await _enterpriseService.SignUp(create);

                    if (isValid)
                    {
                        return RedirectToAction("SuccessSignUp");
                    }
                    ModelState.AddModelError(String.Empty, TempData["message"].ToString());

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
            bool isUpdated = await _enterpriseService.Update(model);

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
                bool isLogin = await _enterpriseService.Login(enterpriseForLogin);
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
            EnterpriseProfileViewModel currentEnterprise = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj);
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
                EnterpriseProfileViewModel currentEnterprise = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj);
                model.EnterpriseId = currentEnterprise.Id;
                bool isCreated = await _userSupportService.CreateUserReport(model);

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
            EnterpriseProfileViewModel currentEnterprise = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpj);
            return View(currentEnterprise);
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
            return View("RedirectSignUp");
        }
        #endregion
    }
}
