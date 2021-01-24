using Final.BusinessLogic;
using Final.BusinessLogic.Concretes;
using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kullanici = Final.Models.Concretes.Kullanici;


namespace FinalMVC_Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {

            return View(SelectCustomerById(id));
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
                if (InsertCustomer(collection["UserName"], collection["CustomerName"], collection["CustomerSurname"], collection["CustomerPasskey"], int.Parse(collection["CustomerTC"])))
                {
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                return View(SelectCustomerById(id));
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
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
                if (UpdateCustomer(id, collection["UserName"], collection["CustomerName"], collection["CustomerSurname"], collection["CustomerPasskey"], int.Parse(collection["CustomerTC"])))
                
                    return RedirectToAction("Index");
                
                return View();
            }
            catch (Exception)
            {

                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteCustomer(id))
                    return RedirectToAction("ListAll");
                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Operation failed!", ex);
            }
        }

        private bool DeleteCustomer(int id)
        {
            try
            {
                using (var customerBussines = new KullaniciBusiness())
                {
                    return customerBussines.DeleteCustomerById(id);
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
                IList<Kullanici> customers = ListAllCustomers().ToList();
                return View(customers);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }

        private List<Kullanici> ListAllCustomers()
        {
            try
            {
                using (var customerBussines = new KullaniciBusiness())
                {
                    List<Kullanici> customers = customerBussines.SelectAllCustomers();
                    return customers;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }

        private bool UpdateCustomer(int id, string username,string name, string surname, string password, int tc)
        {
            try
            {
                using (var customerBusiness = new KullaniciBusiness())
                {
                    return customerBusiness.UpdateCustomer(new Kullanici()
                    {
                        CustomerID = id,
                        UserName=username,
                        CustomerName = name,
                        CustomerPasskey = password,
                        CustomerTC = tc
                    });
                }

            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }
        

        private bool InsertCustomer(string username,string name, string surname, string password, int tc)
        {
            try
            {
                using (var customerBusiness = new KullaniciBusiness())
                {
                    return customerBusiness.InsertKullanici(new Kullanici()
                    {
                        UserName =username,
                        CustomerName = name,
                        CustomerSurname = surname,
                        CustomerPasskey = password,
                        CustomerTC = tc
                    });

                }
            }
            catch (Exception ex)
            {

                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("Customer doesn't exists.");
            }
        }

        private Kullanici SelectCustomerById(int id)
        {
            try
            {
                using (var customerBusiness = new KullaniciBusiness())
                {
                    return customerBusiness.SelectCustomerById(id);
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