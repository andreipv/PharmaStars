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
        // GET: AdminManufacturers
        public async Task<ActionResult> Index()
        {
            ManufacturerService manServ = new ManufacturerService();

            return View(await manServ.GetAll());
        }
    }
}