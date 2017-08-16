using MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AdminCategoriesController : Controller
    {
        // GET: AdminCategories
        public async Task<ActionResult> Index()
        {
            CategoriesService catServ = new CategoriesService();

            return View(await catServ.GetAllCategories());
        }
    }
}