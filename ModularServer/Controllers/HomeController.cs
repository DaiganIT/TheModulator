using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheModulatorServerCommon.Interfaces;

namespace TheModulator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IModuleDetector _moduleDetector;

        public HomeController(IModuleDetector moduleDetector)
        {
            _moduleDetector = moduleDetector;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var modules = _moduleDetector.GetModulesList();
            return View(modules);
        }
    }
}
