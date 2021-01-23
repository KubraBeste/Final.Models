using Final.BusinessLogic.Concretes;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalMVC_Web.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int ManagerID)
        {
            return View(SelectManagerById(ManagerID));
        }

        private ManagerLogin SelectManagerById(int ManagerID)
        {
            try
            {
                using (var managebusiness = new ManageBusiness())
                {
                    return managebusiness.SelectManagerById(ManagerID);
                }
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (InsertManage(collection["ManagerName"], collection["Password"]))
                    return RedirectToAction("Index" , "Home");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        private bool InsertManage(string name, string password)
        {
            try
            {
                using (var magagerBusiness = new ManageBusiness())
                {
                    return magagerBusiness.InsertManage(new ManagerLogin()
                    {
                        ManagerName=name,
                        Password=password

                    });

                }
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }
    }
}