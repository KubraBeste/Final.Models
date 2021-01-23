using Final.BusinessLogic.Concretes;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.DataAccess.Concretes;
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
            SirketRepository sirketrepository = new SirketRepository();
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>>
                (sirketrepository.GetAll());
            return View(objMultipleModels);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create(FormCollection collection)
        {
            return View();
        }
        /*
        private Kiralama kiralama(Kiralama kiralamaid, Arac aracid, Kullanici kullaniciid )
        {
            try
            {
                using (var kiralamabusiness = new KiralamaBusiness())
                {
                    //return kiralamabusiness.Kiralamak(kiralamaid, aracid, kullaniciid);

                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }*/
        
    }
}