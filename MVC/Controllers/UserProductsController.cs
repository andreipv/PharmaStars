using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Services;
using System.Threading.Tasks;
using MVC.Models;

namespace MVC.Controllers
{
    public class UserProductsController : Controller
    {
        private IService<CategoryModel> categoryService;
        private IService<ManufacturerModel> manufacturerService;
        private IService<SimpleProductModel> productService;

        public UserProductsController(IService<CategoryModel> service)
        {
            categoryService = service;
        }

        public UserProductsController(IService<ManufacturerModel> service)
        {
            manufacturerService = service;
        }

        public UserProductsController(IService<SimpleProductModel> service)
        {
            productService = service;
        }

        // GET: UserProducts
        public async Task<ActionResult> Index()
        {
            //CategoriesService cs = new CategoriesService();
            ViewBag.Categories = await categoryService.GetAll();
            
            //ManufacturerService ms = new ManufacturerService();
            ViewBag.Manufacturers = await manufacturerService.GetAll();

            //ProductsService ps = new ProductsService();
            ViewBag.Products = await productService.GetAll();

            return View();
        }
    }
}