using MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AdminProductsController : Controller
    {
        // GET: AdminHome
        public async Task<ActionResult> Index()
        {
            

            ProductsService ps = new ProductsService();
            

            return View(await ps.GetAll());
        }
    }
}