using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Application.Intefaces;
using PI.Application.ViewModel.Enterprise;

namespace PI.Web.Controllers
{
    public class EnterpriseController : Controller
    {
        private IEnterpriseService _enterpriseService;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        //[HttpPost]
        ////public async Task<IActionResult> SignUp(CreateEnterpriseViewModel model)
        ////{
        ////    bool v = await _enterpriseService.SignUp(model);

        ////    return v ? RedirectToAction("Index", "Home") : View();
        ////}

        ////[HttpPost]
        ////public IActionResult Tester(string data)
        ////{
        ////    var item = data;

        ////    return RedirectToAction("Index", "Home");
        ////}

        [HttpPost]
        public async Task<IActionResult> SignUp(string dado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateEnterpriseViewModel create = JsonConvert.DeserializeObject<CreateEnterpriseViewModel>(dado);
                    bool isValid = await _enterpriseService.SignUp(create);

                    if (isValid)
                    {
                        return View(); // Add the redirect to controller of initial page
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

                return View();
            }

            return View();
        }
    }
}
