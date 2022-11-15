using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Enterprise;
using PI.Application.ViewModel.UserSupport;

namespace PI.Web.Controllers
{
    public class EnterpriseController : Controller
    {
        private IEnterpriseService _enterpriseService;
        private readonly IUserSupportService _userSupportService;
        private readonly IUserProblemsCategoryService _userProblemsCategoryService;
        public EnterpriseController(IEnterpriseService enterpriseService, IUserSupportService userSupportService, IUserProblemsCategoryService userProblemsCategoryService)
        {
            _enterpriseService = enterpriseService;
            _userSupportService = userSupportService;
            _userProblemsCategoryService = userProblemsCategoryService;
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
                    Response.Cookies.Append("enterpriseCnpj", login.Login);
                    return View();
                }

                ModelState.AddModelError(nameof(login.Password), "Senha ou usuário incorretos");

                return View();
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Support(int enterpriseId)
        {

            //string enterpriseCnpjAsString = Request.Cookies["enterpriseCnpj"].ToString();

            //EnterpriseProfileViewModel model = await _enterpriseService.GetEnterpriseByCnpj(enterpriseCnpjAsString);

            EnterpriseProfileViewModel model = await _enterpriseService.GetEnterpriseForProfileById(enterpriseId);

            ViewBag.userProblemsCategoriesAsString = await _userProblemsCategoryService.GetNameOfAllAsString();

            AddUserSupportViewModel modelForView = new AddUserSupportViewModel() { EnterpriseId = model.Id, NameOfEnterprise = model.FantasyName };

            return View(modelForView);

        }
    }
}
