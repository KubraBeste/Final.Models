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
    public class SirketBusiness : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool InsertSirket(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
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
        public bool UpdateSirket(Sirket entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
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
        public bool DeleteSirketById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new SirketRepository())
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
        public Sirket SelectSirketById(int sirketId)
        {
            try
            {
                Sirket responseEntitiy;
                using (var repo = new SirketRepository())
                {
                    responseEntitiy = repo.SelectedById(sirketId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Sirket doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<Sirket> SelectAllSirket()
        {
            var responseEntities = new List<Sirket>();

            try
            {
                using (var repo = new SirketRepository())
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
