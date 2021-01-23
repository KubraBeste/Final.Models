using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalMVC_Web.Controllers
{
    public class websiteController : Controller
    {
        // GET: website
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserWebSite()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", " Home");
        }
        public ActionResult ManageWebSite()
        {
            return View();
        }
    }
}