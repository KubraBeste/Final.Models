using Final.BusinessLogic.Concretes;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public ActionResult Details(int id)
        {
            return View(SelectAracById(id));
        }
        private Arac SelectAracById(int id)
        {
            try
            {
                using (var aracBusiness = new AracBusiness())
                {
                    return aracBusiness.SelectAracById(id);
                }
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Arac doesn't exists.");
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
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (InsertArac(int.Parse(collection["AitOlduguSirket"]) , collection["Marka"], collection["Model"], int.Parse(collection["Yıl"]) , bool.Parse(collection["Uygunluk"])))
                    return RedirectToAction("ListAll");

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        private bool InsertArac(int aitoldugusirket, string marka, string model, int yıl, bool uygunluk)
        {
            try
            {
                using (var aracbusiness = new AracBusiness())
                {
                    return aracbusiness.InsertArac(new Arac()
                    {
                        AitOlduguSirket = aitoldugusirket,
                        Marka = marka,
                        Model = model,
                        Yıl = yıl,
                        Uygunluk = uygunluk

                    });
                }

            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Arac doesn't exists.");
            }
        }

        public ActionResult Edit(int id )
        {
            try
            {
                return View(SelectAracById(id));
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Arac doesn't exists.");
            }
        }
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (UpdateArac(id, int.Parse(collection["AitOlduguSirket"]), collection["Marka"], collection["Model"], int.Parse(collection["Yıl"]), bool.Parse(collection["Uygunluk"]), decimal.Parse(collection["Günlük Fiyat"])))
                    return RedirectToAction("Index");
                return View();
            }
            catch (Exception)
            {

                return View();
            }
        }


        private bool UpdateArac(int id, int v1, string v2, string v3, int v4, bool v5 , decimal v6)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteArac(id))
                    return RedirectToAction("ListAll");
                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Operation failed!", ex);
            }
        }

        private bool DeleteArac(int id)
        {
            try
            {
                using (var aracbusiness = new AracBusiness())
                {
                    return aracbusiness.DeleteAracById(id);
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
                throw new Exception("Customer doesn't exists.");
            }
        }

        private List<Arac> ListAllArac()
        {
            try
            {
                using (var aracBusiness = new AracBusiness())
                {
                    List<Arac> arac = aracBusiness.SelectAllArac();
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