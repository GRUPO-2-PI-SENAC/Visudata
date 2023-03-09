using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PI.Domain.Entities;

namespace PI.Web.Util
{
    public class BaseController : Controller
    {
        public Enterprise CurrentEnterprise
        {
            get
            {
                string currentEnterpriseAsString = HttpContext.Session.GetString("currentEnterprise") ?? "";
                Enterprise currentEnterprise = JsonConvert.DeserializeObject<Enterprise>(currentEnterpriseAsString) ?? new Enterprise();

                return currentEnterprise;
            }
            set => CurrentEnterprise = value;
        }
    }
}
