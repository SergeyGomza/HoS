using System.Web.Mvc;
using HoS_AP.DAL.ServiceInterfaces;

namespace HoS_AP.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;

        [Route]
        public ActionResult Login()
        {
            return View();
        }

        [Route, HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (accountService.Authenticate(username, password))
            {
                
            }

            return View();
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}