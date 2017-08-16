using MVC.Models;
using MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationService service;

        public AuthenticationController(IAuthenticationService service)
        {
            this.service = service;
        }

        public AuthenticationController() { }

        public ActionResult Login()
        {
            return View();
        }

        [ActionName("Login"), HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginPost(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //AuthenticationService service = new AuthenticationService();
                string token = await service.Login(model);

                Response.Cookies["user"]["token"] = token;
            }

            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [ActionName("Register"), HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPost(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //AuthenticationService service = new AuthenticationService();
                    await service.Register(model);

                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    return RedirectToAction("Register");
                }
                
            }

            return RedirectToAction("Register");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [ActionName("ForgotPassword"), HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPasswordPost(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //AuthenticationService service = new AuthenticationService();
                    await service.ForgotPassword(model);

                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    return RedirectToAction("ForgotPassword");
                }

            }

            return RedirectToAction("ForgotPassword");
        }
    }
}
