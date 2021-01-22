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
    public class AracBusiness : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool InsertArac(Arac entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
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

        

        public bool UpdateArac(Arac entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
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
        public bool DeleteAracById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new AracRepository())
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
        public Arac SelectAracById(int aracId)
        {
            try
            {
                Arac responseEntitiy;
                using (var repo = new AracRepository())
                {
                    responseEntitiy = repo.SelectedById(aracId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Arac doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<Arac> SelectAllArac()
        {
            var responseEntities = new List<Arac>();

            try
            {
                using (var repo = new AracRepository())
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

    }
}
