using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Services;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class UserProductsController : Controller
    {
        // GET: UserProducts
        public async Task<ActionResult> Index()
        {
            CategoriesService cs = new CategoriesService();
            ViewBag.Categories = await cs.GetAllCategories();
            
            ManufacturerService ms = new ManufacturerService();
            ViewBag.Manufacturers = await ms.GetAll();

            ProductsService ps = new ProductsService();
            ViewBag.Products = await ps.GetAll();

            return View();
        }
    }
}