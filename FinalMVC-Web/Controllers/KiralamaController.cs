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
    public class KiralamaController : Controller
    {
        // GET: Kiralama
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Create()
        {
           
            return View();
        }
        /*
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
                if (InsertKiralama(int.Parse(collection["KiralayanKisi"]), int.Parse(collection["KiralananArac"])))
                    return View();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        */
        private bool InsertKiralama(int v1, int v2)
        {
            try
            {
                using (var kiralamabusiness = new KiralamaBusiness())
                {
                    return kiralamabusiness.Kiralamak(new Kiralama()
                    {
                        KiralananArac = v2,
                        KiralayanKisi = v1,
                        
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}