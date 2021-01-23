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
    public class ManageBusiness : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
        public bool InsertManage(ManagerLogin entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new ManageRepository())
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
        public ManagerLogin SelectManagerById(int manageId)
        {
            try
            {
                ManagerLogin responseEntitiy;
                using (var repo = new ManageRepository())
                {
                    responseEntitiy = repo.SelectedById(manageId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Manage doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CustomerBusiness::SelectCustomerById::Error occured.", ex);
            }
        }
    }
}
