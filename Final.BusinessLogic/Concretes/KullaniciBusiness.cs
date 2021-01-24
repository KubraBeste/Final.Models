using Final.Commons.Concretes.Helpers;
using Final.Commons.Concretes.Logger;
using Final.DataAccess.Concretes;
using Final.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Final.BusinessLogic.Concretes
{
    public class KullaniciBusiness : IDisposable
    {
        public bool InsertKullanici(Kullanici entity)
        {
           
            try
            {
                bool isSuccess;
                using (var repo = new KullaniciRepository())
                {
                    isSuccess = repo.Insert(entity);
                   
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::InsertCustomer::Error occured.", ex);
            }
        }
        public bool UpdateCustomer(Kullanici entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new KullaniciRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::UpdateCustomer::Error occured.", ex);
            }
        }
        public bool DeleteCustomerById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new KullaniciRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::DeleteCustomer::Error occured.", ex);
            }
        }
        public Kullanici SelectCustomerById(int kullaniciId)
        {
            try
            {
                Kullanici responseEntitiy;
                using (var repo = new KullaniciRepository())
                {
                    responseEntitiy = repo.SelectedById(kullaniciId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<Kullanici> SelectAllCustomers()
        {
            var responseEntities = new List<Kullanici>();

            try
            {
                using (var repo = new KullaniciRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectAllCustomers::Error occured.", ex);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
