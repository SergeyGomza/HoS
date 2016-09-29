using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;
using System.Web.Mvc;
using System.Web.Security;

namespace HoS_AP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web;

    using HoS_AP.Web.Filters;

    //[Language]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [Route]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Character");
            }

            return View(new AuthenticationModel());
        }

        [Route, HttpPost]
        public ActionResult Login(AuthenticationModel model)
        {
            var operationResult = accountService.Authenticate(model);
            if (operationResult.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("Index", "Character");
            }

            operationResult.ToModelErrors(ModelState);
            return View(model);
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
        [Route("Account/ChangeLanguage")]
        public ActionResult ChangeLanguage(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // list of cultures
            List<string> cultures = new List<string> { "ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            // save selected culture in cookies
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang")
                {
                    HttpOnly = false, 
                    Value = lang, 
                    Expires = DateTime.Now.AddYears(1)
                };
            }

            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }
    }
}