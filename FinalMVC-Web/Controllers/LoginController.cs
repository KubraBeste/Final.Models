using Final.BusinessLogic.Concretes;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalMVC_Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        
       
        [AllowAnonymous]
        public ActionResult ManagerLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth( Kullanici kullanici)
        {
            var username = kullanici.UserName;
            var password = kullanici.CustomerPasskey;
            

            if (username == null || password == null)
            {

                kullanici.ErrorMessage = "Wrong username or password";
                return View("UserLogin","Login");
            }
            else
            {
                
                Session["CustomerID"] = kullanici.CustomerID;
                return RedirectToAction("UserWebSite", "website" );
            }
            
            
        }

        [HttpPost]

        public ActionResult MAutherize(Final.Models.Concretes.ManagerLogin manager)
        {

            var username = manager.ManagerName;
            var password = manager.Password;
            if (username == null || password == null)
            {
                manager.ErrorMessage = "Wrong username or password";
                return View("ManagerLogin", manager);
            }
            else
            {
                Session["ManagerID"] = manager.ManagerID;
                
                return RedirectToAction("ManageWebSite", "website" );
            }


        }
       
        


    }
}