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
                AuthenticationService service = new AuthenticationService();
                try
                {
                    string token = await service.Login(model);

                    Response.Cookies["user"]["token"] = token;

                    TempData["error"] = null;
                    return RedirectToAction("Index", "UserProducts");
                } catch(Exception e)
                {
                    TempData["error"] = e.Message;
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["error"] = "Information is invalid!";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            try
            {
                Response.Cookies["user"]["token"] = null;
                Response.Cookies["user"].Expires = DateTime.Now.Subtract(TimeSpan.FromDays(2));

                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
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
                    AuthenticationService service = new AuthenticationService();
                    await service.Register(model);

                    TempData["error"] = null;
                    return RedirectToAction("Login");
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                    return RedirectToAction("Register");
                }
            }
            else
            {
                TempData["error"] = "Information is invalid!";
                return RedirectToAction("Register");
            }
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
                    AuthenticationService service = new AuthenticationService();
                    await service.ForgotPassword(model);

                    TempData["error"] = null;
                    return RedirectToAction("Login");
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                    return RedirectToAction("ForgotPassword");
                }
            }
            else
            {
                TempData["error"] = "Information is invalid!";
                return RedirectToAction("ForgotPassword");
            }
        }

        public ActionResult ResetPassword(string code)
        {
            TempData["error"] = null;

            ViewBag.Code = code;
            return View();
        }

        [ActionName("ResetPassword"), HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPasswordPost(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuthenticationService service = new AuthenticationService();
                    await service.ResetPassword(model);

                    TempData["error"] = null;
                    return RedirectToAction("Login");
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["error"] = "Information is invalid!";
                return RedirectToAction("ForgotPassword");
            }
        }
    }
}
