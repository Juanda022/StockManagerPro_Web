using StockManagerPro_Web.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockManager.Controllers
{
    public class HomeController : Controller
    {
        [ValidateSession]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WithoutPermission()
        {
            ViewBag.Message = "You do not have permissions to access";
            return View();
        }

        public ActionResult SingOff()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Access");
        }
    }
}