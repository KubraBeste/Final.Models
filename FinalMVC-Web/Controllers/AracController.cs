using Final.BusinessLogic.Concretes;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalMVC_Web.Controllers
{
    public class AracController : Controller
    {
        // GET: Arac
        public ActionResult Index()
        {
            return View();
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
                if (InsertCar(collection["Marka"], collection["Model"], int.Parse(collection["Yıl"]), Decimal.Parse(collection["GünlükFiyat"]), int.Parse(collection["AitOlduguSirket"])))
                    return RedirectToAction("ListAll");
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }

        private bool InsertCar(string marka, string model, int yıl,  decimal fiyat, int aitoldugusirket)
        {
            try
            {
                using (var aracbusiness = new AracBusiness())
                {
                    return aracbusiness.InsertArac(new Arac()
                    {
                        Marka = marka,
                        Model=model,
                        Yıl=yıl,
                        GünlükFiyat=fiyat,
                        AitOlduguSirket=aitoldugusirket

                    });
                   
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");


            }
            
        }
        public ActionResult ListAll()
        {
            try
            {
                IList<Arac> arac = ListAllArac().ToList();
                return View(arac);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Arac doesn't exists.");
            }
        }

        private List<Arac> ListAllArac()
        {
            try
            {
                using (var aracbusiness = new AracBusiness())
                {
                    List<Arac> arac = aracbusiness.SelectAllArac();
                    return arac;
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