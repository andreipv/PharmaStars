﻿using MVC.Models;
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
                    AuthenticationService service = new AuthenticationService();
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
    }
}
