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
    public class AdminManufacturersController : Controller
    {
        IService<ManufacturerModel> service;

        public AdminManufacturersController(IService<ManufacturerModel> service)
        {
            this.service = service;
        }

        // GET: AdminManufacturers
        public async Task<ActionResult> Index()
        {
            ViewBag.Manufacturers = await service.GetAll();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await service.Get(id));
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, ManufacturerModel model)
        {
            await service.Put(model.ID, model);

            return RedirectToAction("Index");
        }
    }
}