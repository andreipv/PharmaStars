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
        private IProductService productService;

        public UserProductsController(IService<CategoryModel> service, IService<ManufacturerModel> manufacturerService, IProductService productService)
        {
            categoryService = service;
            this.manufacturerService = manufacturerService;
            this.productService = productService;
        }

        // GET: UserProducts
        public async Task<ActionResult> Index(String searchString)
        {
            ViewBag.Categories = await categoryService.GetAll();
           
            ViewBag.Manufacturers = await manufacturerService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
                ViewBag.Products = await productService.GetAll(searchString);
            else ViewBag.Products = await productService.GetAll();

            return View();
        }
    }
}