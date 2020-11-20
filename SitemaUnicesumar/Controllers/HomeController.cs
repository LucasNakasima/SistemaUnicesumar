using SitemaUnicesumar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitemaUnicesumar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageKey = "Home";

            if (Session["userId"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}