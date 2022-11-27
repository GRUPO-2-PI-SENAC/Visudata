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
                        return View("CreateEnterpriseSuccessed");
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
        public async Task<IActionResult> Login(EnterpriseLoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                bool isLogin = await _enterpriseService.Login(login);
                if (isLogin)
                {
                    TempData["message"] = "Usuario adicionado com sucesso!";
                    Response.Cookies.Append("enterpriseCnpj", login.Login);
                    return RedirectToAction("Home");
                }

                ModelState.AddModelError(nameof(login.Password), "Senha ou usuário incorretos");

                return View();
            }
            TempData["message"] = "Senha ou usuario invalidos";
            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> Support()
        {

            //string enterpriseCnpjAsString = Request.Cookies["enterpriseCnpj"].ToString();

            //EnterpriseProfileViewModel model = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpjAsString);

            //EnterpriseProfileViewModel model = await _enterpriseService.GetEnterpriseForProfileById(enterpriseId);

            ViewBag.userProblemsCategoriesAsString = await _userProblemsCategoryService.GetNameOfAllAsString();

            AddUserSupportViewModel modelForView = new AddUserSupportViewModel() { EnterpriseId = 2, NameOfEnterprise = "Rolo doido produções" };

            return View(modelForView);

        }

        [HttpPost]
        public async Task<IActionResult> Support(AddUserSupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _userSupportService.CreateUserReport(model);

                if (isCreated)
                {
                    TempData["message"] = "Relato enviado com sucesso!";
                    return View();
                }
                TempData["message"] = "Näo foi possível adicionar esse relato , tente novamente mais tarde ou entre em contato aocm o administrador";

                return View(); // see what's pages will be recieve this content about this problem 
            }

            return View(model);
        }
        #endregion

    }
}
