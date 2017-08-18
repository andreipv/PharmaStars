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
    public class AdminCategoriesController : Controller
    {
        IService<CategoryModel> service;

        public AdminCategoriesController(IService<CategoryModel> service)
        {
            this.service = service;
        }

        // GET: AdminCategories
        public async Task<ActionResult> Index()
        {
            ViewBag.Categories = await service.GetAll();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await service.Get(id));
        }

        [HttpPut]
        public async Task<ActionResult> Edit(CategoryModel category)
        {
            await service.Put(category.ID, category);

            return RedirectToAction("Index");
        }
    }
}