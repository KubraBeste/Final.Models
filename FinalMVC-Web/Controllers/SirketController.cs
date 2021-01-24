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
    public class SirketController : Controller
    {
        // GET: Sirket
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
                if (InsertSirket(collection["SirketAdi"], collection["SirketAdres"], int.Parse(collection["AracSayisi"]), decimal.Parse(collection["SirketPuani"])))
                     return RedirectToAction("ListAll");



                return View();
            }
            catch (Exception ex )
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }

        private bool InsertSirket(string sirketadi, string sirketadres, int aracsayisi, decimal sirketpuani)
        {
            try
            {
                using (var sirketbusiness = new SirketBusiness())
                {
                    return sirketbusiness.InsertSirket(new Sirket()
                    {
                        SirketAdi = sirketadi,
                        SirketAdres = sirketadres,
                        AracSayisi = aracsayisi,
                        SirketPuani = sirketpuani

                    }) ;
                    
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
                IList<Sirket> sirkets = ListAllSirket().ToList();
                return View(sirkets);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("sirket doesn't exists.");
            }
        }

        private List<Sirket> ListAllSirket()
        {
            try
            {
                using (var sirketbusiness = new SirketBusiness())
                {
                    List<Sirket> sirkets = sirketbusiness.SelectAllSirket();
                    return sirkets;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }
        public ActionResult Details(int id)
        {

            return View(SelectSirketById(id));
        }

        private object SelectSirketById(int id)
        {
            try
            {
                using (var sirketbusiness = new SirketBusiness())
                {
                    return sirketbusiness.SelectSirketById(id);
                }
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteSirket(id))
                    return RedirectToAction("ListAll"); 

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Operation failed!", ex);
            }
        }

        private bool DeleteSirket(int id)
        {
            try
            {
                using (var sirketbusiness = new SirketBusiness())
                {
                    return sirketbusiness.DeleteSirketById(id);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Sirket doesn't exists.");
            }
        }
    }
}