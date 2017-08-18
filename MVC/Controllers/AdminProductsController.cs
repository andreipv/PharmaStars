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
    public class AdminProductsController : Controller
    {

        IProductService service;
        IService<ManufacturerModel> manufacturerService;
        IService<CategoryModel> categoryService;

        public AdminProductsController(IProductService service, IService<ManufacturerModel> manufacturerService, IService<CategoryModel> categoryService) 
        {
            this.service = service;
            this.manufacturerService = manufacturerService;
            this.categoryService = categoryService;
        }
        // GET: AdminHome
        public async Task<ActionResult> Index()
        {
            ViewBag.Products = await service.GetAll();
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Add()
        {
            
            ViewBag.Dropdown = new SelectList(await manufacturerService.GetAll(), "ID", "Name");
            ViewBag.Categories = new MultiSelectList(await categoryService.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(FullProductModel model)
        {
            await service.Post(model);

            return RedirectToAction("Index");
        }
    }
}